using UnityEngine;
using System.Collections;
using TMPro;
using System;


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
        // ToDo: Remove this and set this.walls = walls
        Wall[] shrinked = new Wall[15];
        for (int i = 0; i < 15; i++)
        {
            shrinked[i] = walls[i];
        }

        this.walls = shrinked;
        this.photos = photos;
        this.algorithm = algorithm;
    }

    public void GenerateLayout()
    {
        var debug = GameObject.Find("TextBox").GetComponent<TextMeshProUGUI>();
        int line = 1;
        try{
       
        debug.text = "Just Generating Layout";

        LayoutAlgorithm algo = new LayoutAlgorithm();
        //NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        line = line + 1;
        debug.text = "Generating Layout";

        Layout[] layouts = new Layout[walls.Length];
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
            debug.text = "Wall " + i + " of " + walls.Length + " done";
        }
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length + "drawing";
            if (layouts[i] != null){
                layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i]);
            } else {
                line = -1000000000;
            }
            Debug.Log("Layout " + i + " has angles" + walls[i].rotationAngles);
            debug.text = "Wall " + i + " of " + walls.Length + "done drawing";
        }	
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].Draw();
        }
        }
        catch(Exception ex){
            //throw divide by zero exception
            debug.text = "Generate Layout : Error on line " + line + " " + ex;
        }
        


       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
