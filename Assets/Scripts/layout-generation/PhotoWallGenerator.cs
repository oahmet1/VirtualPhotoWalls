using UnityEngine;

public class Layout
{
    private int[] indices;
    public Layout(int[] indices)
    {
        this.indices = indices;
    }

    public void Draw(Photograph[] photos){
        for (int i = 0; i < this.indices.Length; i++)
        {
            photos[this.indices[i]].Draw();
        }
    }
}

public class PhotoWallGenerator
{
    private Walls[] walls;
    private Photograph[] photos;

    public PhotoWallGenerator(Wall[] walls, Photograph[] photos, String algorithm)
    {
        this.walls = walls;
        this.photos = photos;
        this.algorithm = algorithm;
    }

    void GenerateLayout()
    {
        NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        Layout[] layouts = new Layouts[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            //layouts[i] = algo.GenerateLayout(walls[i], photos);
        }

        for (Wall wall : walls)
        {
            wall.Draw();
        }

        //for (Layout layout : layouts)
        //{
        //    layout.Draw(photos);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
