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
    public GameObject textmesh;

    public PhotoWallGenerator(Wall[] walls, Photograph[] photos, string algorithm, GameObject textmesh)
    {
        this.walls = walls;
        this.photos = photos;
        this.algorithm = algorithm;
        this.textmesh = textmesh;
        textmesh.GetComponent<TextMesh>().text = "Exit constructor";

    }

    public void GenerateLayout()
    {
        this.textmesh.GetComponent<TextMesh>().text = "Just Generating Layout";
        NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        this.textmesh.GetComponent<TextMesh>().text = "Generating Layout";
        Layout[] layouts = new Layout[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            this.textmesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
            this.textmesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + " done";
        }

        for (int i = 0; i < walls.Length; i++)
        {
            this.textmesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + "drawing";
            layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i]);
            Debug.Log("Layout " + i + " has angles" + walls[i].rotationAngles);
            this.textmesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + "done drawing";
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
