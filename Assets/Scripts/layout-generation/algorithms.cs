
interface ILayoutAlgorithm {
    void GenerateLayout(Photograph[] photos, Wall wall);
}

class RandomLayoutAlgorithm : ILayoutAlgorithm {
    public void GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();
        float[] boundsZ = wall.GetBoundsZ();

        for (int i=0; i < photos.Length; i++) {
            float x = boundsX[0] + (float)rnd.NextDouble() * (-boundsX[0] + boundsX[1]);
            float y = boundsY[0] + (float)rnd.NextDouble() * (-boundsY[0] + boundsY[1]);
            float z = boundsZ[0] + (float)rnd.NextDouble() * (-boundsZ[0] + boundsZ[1]);

            photos[i].SetPosition(x, y, z);
            photos[i].SetDisplayed(true);
        }
    }
} 

class NoOverlapRandomLayoutAlgorithm : ILayoutAlgorithm {
    public void GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();
        float[] boundsZ = wall.GetBoundsZ();

        for (int i=0; i < photos.Length; i++) {
            float x = boundsX[0] + (float) rnd.NextDouble()*(-boundsX[0] + boundsX[1]);
            float y = boundsY[0] + (float)rnd.NextDouble() * (-boundsY[0] + boundsY[1]);
            float z = boundsZ[0] + (float)rnd.NextDouble() * (-boundsZ[0] + boundsZ[1]);

            bool valid = true;
            for (int j=0; j<i; j++){
                if (photos[j].IsOverlapping(photos[i])){
                    valid = false;
                }
            }
            if (valid){
                photos[i].SetPosition(x, y, z);
                photos[i].SetDisplayed(true);
            }
        }
    }
}