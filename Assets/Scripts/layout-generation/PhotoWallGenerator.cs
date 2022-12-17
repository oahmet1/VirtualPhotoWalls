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

public class PhotoWallGenerator : MonoBehaviour
{
    private Wall[] walls;
    private Photograph[] photos;

    private string algorithm;

    public PhotoWallGenerator(Wall[] walls, Photograph[] photos, string algorithm, GameObject DebugTextMesh)
    {
        this.walls = walls;
        this.photos = photos;
        this.algorithm = algorithm;
        //DebugTextMesh.GetComponent<TextMesh>().text = "Exit constructor";

    }

    public void GenerateLayout(GameObject text_mesh)
    {
        text_mesh.GetComponent<TextMesh>().text = "Just Generating Layout";
        NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        text_mesh.GetComponent<TextMesh>().text = "Generating Layout";
        Layout[] layouts = new Layout[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            text_mesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
            text_mesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + " done";
        }

        for (int i = 0; i < walls.Length; i++)
        {
            text_mesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + "drawing";
            layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i]);
            Debug.Log("Layout " + i + " has angles" + walls[i].rotationAngles);
            text_mesh.GetComponent<TextMesh>().text = "Wall " + i + " of " + walls.Length + "done drawing";
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
