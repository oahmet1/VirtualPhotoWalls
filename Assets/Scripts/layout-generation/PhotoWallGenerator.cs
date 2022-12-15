using UnityEngine;
using System.Collections;

public class Layout
{
    private Photograph[] photos;

    public Layout(Photograph[] photos)
    {
        this.photos = photos;
    }

    public void Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall){
        foreach(Photograph photo in photos){
            photo.Draw(rotationAngles, centerCoordinates, parent, wall);
        }
    }
}

public class PhotoWallGenerator
{
    private Wall[] walls;
    private Photograph[] photos;

    private string algorithm;

    public PhotoWallGenerator(Wall[] walls, Photograph[] photos, string algorithm)
    {
        this.walls = walls;
        this.photos = photos;
        this.algorithm = algorithm;
    }

    public void GenerateLayout()
    {
        LayoutAlgorithm algo = new LayoutAlgorithm();
        Layout[] layouts = new Layout[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
        }

        for (int i = 0; i < walls.Length; i++)
        {

            layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i]);
            Debug.Log("Layout " + i + " has angles" + walls[i].rotationAngles);
        }	

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].Draw();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
