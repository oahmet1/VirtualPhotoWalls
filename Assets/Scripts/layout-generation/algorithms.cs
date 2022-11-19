using game_objects.Photograph;
using game_objects.Wall;

interface ILayoutAlgorithm {
    void GenerateLayout(Photgraph[] photos, Wall wall);
}

class RandomLayoutAlgorithm : ILayoutAlgorithm {
    public void GenerateLayout(Photograph[] photos, Wall wall) {
        System.Random rnd = new System.Random();

        float[] boundsX = wall.GetBoundsX();
        float[] boundsY = wall.GetBoundsY();
        float[] boundsZ = wall.GetBoundsZ();

        for (int i=0; i < photos.Length; i++) {
            float x = rnd.Next(-boundsX[0], boundsX[1]);
            float y = rnd.Next(-boundsY[0], boundsY[1]);
            float z = rnd.Next(-boundsZ[0], boundsZ[1]);

            photos[i].SetPosition(x, y, z);
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
            float x = rnd.Next(-boundsX[0], boundsX[1]);
            float y = rnd.Next(-boundsY[0], boundsY[1]);
            float z = rnd.Next(-boundsZ[0], boundsZ[1]);

            bool valid = true;
            for (int j=0; j<i; j++){
                if (photos[j].IsOverlapping(photos[i])){
                    valid = false;
                }
            }
            if (valid){
                photos[i].SetPosition(x, y, z);
                photos[i].displayed = true;
            }
        }
    }
}