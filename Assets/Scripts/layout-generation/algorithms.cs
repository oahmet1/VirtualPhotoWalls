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
            float x = boundsX[0] + (float) rnd.NextDouble() * (-boundsX[0] + boundsX[1]);
            float y = boundsY[0] + (float)rnd.NextDouble() * (-boundsY[0] + boundsY[1]);

            bool valid = true;
            for (int j=0; j<i; j++){
                if (photos[j].IsOverlapping(photos[i])){
                    valid = false;
                }
            }
            if (valid){
                Photograph copy = photos[i].createCopy();
                copy.SetPosition(x, y, 1);
                displayedPhotos.Add(copy);
            }
        }
        return new Layout(displayedPhotos.ToArray(typeof(Photograph)) as Photograph[]);
    }
    
}