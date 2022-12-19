using System.Collections;
using UnityEngine;
interface ILayoutAlgorithm {
    Layout GenerateLayout(Photograph[] photos, Wall wall, int NumberofPhotosEachWall, int AlgorithmParameter);
}

class NoOverlapRandomLayoutAlgorithm : ILayoutAlgorithm {
    public Layout GenerateLayout(Photograph[] photos, Wall wall, int NumberofPhotosEachWall, int AlgorithmParameter) 
    {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();


        ArrayList displayedPhotos = new ArrayList();

        int numphotos = Mathf.Min(NumberofPhotosEachWall, photos.Length);
        for (int i=0; i < numphotos; i++) {
            float width = photos[i].width;
            float height = photos[i].height;
            if(-boundsX[0] + boundsX[1] - 2*width < 0 || -boundsY[0] + boundsY[1] - 2*height < 0){
                //Debug.Log("Not enough space to display all photos");
                break;
            }
            float x = boundsX[0]+width + (float) rnd.NextDouble() * (-boundsX[0] + boundsX[1] - 2*width);
            float y = boundsY[0]+height + (float)rnd.NextDouble() * (-boundsY[0] + boundsY[1] - 2*height);
            photos[i].SetPosition(x, y, 0.1f);

            bool valid = true;
            for (int j=0; j<i; j++){
                if (photos[j].IsOverlapping(photos[i])){
                    valid = false;
                }
            }
            if (valid){
                Photograph copy = photos[i].createCopy();
                copy.SetPosition(x, y, 0.1f);
                displayedPhotos.Add(copy);
            }
        }
        return new Layout(displayedPhotos.ToArray(typeof(Photograph)) as Photograph[]);
    }
    
}

class LayoutAlgorithm : ILayoutAlgorithm {
    private void placePhotosOnCircle(Wall wall, Photograph[] photos, ArrayList prevPhotos, float radius, float centerX, float centerY){
        float margin = 0.05f;
        float angle = Random.Range(0, 2 * Mathf.PI);
        float numSteps  =  Mathf.CeilToInt(2*radius / photos[0].width + 2*radius / photos[0].height);
        float angleStep = 2 * Mathf.PI / numSteps; // ToDo: figure out good heuristic for the angle step with increasing circle diameter
        int numPhotos = photos.Length;
        for (int i=0; i<numSteps; i++){
            float x = centerX + radius * Mathf.Cos(angle);
            float y = centerY + radius * Mathf.Sin(angle);
            // ToDo: some positions on the circle might need to be skipped
            int randomPhotoIndex = Random.Range(0, numPhotos);
            Photograph nextPhoto = photos[randomPhotoIndex % numPhotos].createCopy();
            nextPhoto.SetPosition(x, y, 0.1f);
            float moveX = 0;
            float moveY = 0;
            bool validPlacementFound = true;
            for (int j=0; j<prevPhotos.Count; j++){
                Photograph prevPhoto = prevPhotos[j] as Photograph;
                if (prevPhoto.IsOverlappingWithMargin(nextPhoto, margin)){
                    float[] prevPos = prevPhoto.GetPosition();
                    float prevWidth = prevPhoto.width;
                    float prevHeight = prevPhoto.height;
                    float nextWidth = nextPhoto.width;
                    float nextHeight = nextPhoto.height;

                    // calculate how much we need to move the next photo to avoid overlap
                    float newMoveX = 0;
                    float newMoveY = 0;
                    float spaceNeededX = prevWidth/2 + nextWidth/2 + margin - Mathf.Abs(x - prevPos[0]);
                    float spaceNeededY = prevHeight/2 + nextHeight/2 + margin - Mathf.Abs(y - prevPos[1]);
                    if (x < prevPos[0]){
                        newMoveX = -spaceNeededX;
                    } else {
                        newMoveX = spaceNeededX;
                    }
                    if (y < prevPos[1]){
                        newMoveY = -spaceNeededY;
                    } else {
                        newMoveY = spaceNeededY;
                    }

                    // choose the best direction to move the photo
                    if ((moveX * newMoveX >= 0 && Mathf.Abs(moveX) > Mathf.Abs(newMoveX)) || (moveY * newMoveY >= 0 && Mathf.Abs(moveY) > Mathf.Abs(newMoveY))){
                        // nothing to do
                        continue;
                    } else if (Mathf.Abs(newMoveX) < Mathf.Abs(newMoveY) && moveX * newMoveX >= 0){
                        // prefer move in x direction and it is possible
                        moveX = newMoveX;
                    } else if (moveY * newMoveY >= 0){
                        // prefer move in y direction and it is possible
                        moveY = newMoveY;
                    } else if (moveX * newMoveX >= 0) {
                        moveX = newMoveX;
                    } else {
                        // move is not possible in either direction
                        validPlacementFound = false;
                        break;
                    }
                }
            }
            nextPhoto.SetPosition(x + moveX, y + moveY, 0.1f);
            if (!validPlacementFound || !wall.photoIsInsideWall(nextPhoto, new float[]{0.25f, 0.25f, 0.25f, 0.25f})){
                continue;
            }
            prevPhotos.Add(nextPhoto);
            angle += angleStep;
            angle %= 2 * Mathf.PI;
        }
    }
    public Layout GenerateLayout(Photograph[] photos, Wall wall, int NumberofPhotosEachWall, int AlgorithmParameter)
    {
        
        System.Random rnd = new System.Random();
        //get the first image randomly and place it in the middle of the wall
        ArrayList displayedPhotos = new ArrayList();
        ArrayList notDisplayedPhotos = new ArrayList(photos);

        int numphotos = Mathf.Min(NumberofPhotosEachWall, photos.Length);
        for (int i=0; i< numphotos; i++){
            notDisplayedPhotos.Add(photos[i]);
        }

        int firstPhotoInd = rnd.Next(photos.Length);
        DisplayAPhoto(displayedPhotos, notDisplayedPhotos, photos[firstPhotoInd], 0, 0);
        
        float r = GetRadius(displayedPhotos, wall);
        int numCircles = 0;
        while (r <  Mathf.Min(wall.height/2, wall.width/2) && numCircles <= AlgorithmParameter)
        {
            placePhotosOnCircle(wall, photos, displayedPhotos, r, 0, 0);
            r = GetRadius(displayedPhotos, wall);
            numCircles++;
        }
        
        return new Layout(displayedPhotos.ToArray(typeof(Photograph)) as Photograph[]);
    }

    public void DisplayAPhoto(ArrayList displayedPhotos, ArrayList notDisplayedPhotos, Photograph p , float centerX, float centerY){
        p.SetPosition(centerX, centerY, 0.1f);
        displayedPhotos.Add(p.createCopy());
        notDisplayedPhotos.Remove(p);
    }

    public float GetRadius(ArrayList displayedPhotos, Wall wall){
        if(displayedPhotos.Count == 0){
            return 0;
        }
        Photograph centerPhoto = displayedPhotos[0] as Photograph;
        if(displayedPhotos.Count == 1){
            
            return(Mathf.Max(centerPhoto.width/2, centerPhoto.height/2));
        }
        
        float maxDist = 0;
        
        for(int i=1; i<displayedPhotos.Count; i++){
            Photograph p = displayedPhotos[i] as Photograph;
            float xDelta = centerPhoto.x - p.x;
            float yDelta = centerPhoto.y - p.y;
            
            float cornerX = 0;
            float cornerY = 0;

            if(xDelta >= 0 && yDelta >= 0){
                cornerX = p.x - p.width/2;
                cornerY = p.y - p.height/2;
            }else if(xDelta >= 0 && yDelta < 0){
                cornerX = p.x - p.width/2;
                cornerY = p.y + p.height/2;
            }else if(xDelta < 0 && yDelta >= 0){
                cornerX = p.x + p.width/2;
                cornerY = p.y - p.height/2;
            }else if(xDelta < 0 && yDelta < 0){
                cornerX = p.x + p.width/2;
                cornerY = p.y + p.height/2;
            }
            float distance = Mathf.Pow(centerPhoto.x - cornerX, 2) + Mathf.Pow(centerPhoto.y - cornerY, 2);
            maxDist = Mathf.Max(maxDist, distance);
        }
        return Mathf.Sqrt(maxDist);

    }
        
    
}
