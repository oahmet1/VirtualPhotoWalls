using UnityEngine;
using System.Collections;

public class Layout
{
    private Photograph[] photos;

    public Layout(Photograph[] photos)
    {
        this.photos = photos;
    }

    public void Draw(Transform transform){
        foreach(Photograph photo in photos){
            photo.Draw(transform);
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
        NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        Layout[] layouts = new Layout[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
        }

        Transform[] wallTransforms = new Transform[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            wallTransforms[i] = walls[i].Draw();
        }

        for (int i = 0; i < walls.Length; i++)
        {

            layouts[i].Draw(wallTransforms[i]);
            Debug.Log("Layout " + i + " has " + wallTransforms[i].localScale);
        }	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
