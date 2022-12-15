using System.Collections;
interface ILayoutAlgorithm {
    Layout GenerateLayout(Photograph[] photos, Wall wall);
}

class RandomLayoutAlgorithm {
    public void GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();

        for (int i=0; i < photos.Length; i++) {
            float x = boundsX[0] + (float)rnd.NextDouble() * (-boundsX[0] + boundsX[1]);
            float y = boundsY[0] + (float)rnd.NextDouble() * (-boundsY[0] + boundsY[1]);

            photos[i].SetPosition(x, y, 0);
        }
    }
} 

class NoOverlapRandomLayoutAlgorithm : ILayoutAlgorithm {
    public Layout GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();


        ArrayList displayedPhotos = new ArrayList();

        for (int i=0; i < photos.Length; i++) {
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

class LayoutAlgrithm : ILayoutAlgorithm {
    public Layout GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

    }
    
}