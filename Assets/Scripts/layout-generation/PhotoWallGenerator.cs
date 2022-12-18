using UnityEngine;
using System.Collections;
using TMPro;
using System;


public class Layout
{
    public Photograph[] photos;

    public Layout(Photograph[] photos)
    {
        this.photos = photos;
    }

    public void  Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall, GameObject PhotoPrefab){
        foreach(Photograph photo in photos){
            photo.Draw(rotationAngles, centerCoordinates, parent, wall, PhotoPrefab);
        }
    }
}

public enum AlgorithmTypes 
{
    RandomLayout = 0,
    CircularLayout= 1
}
public class PhotoWallGenerator : MonoBehaviour
{
 
    public int numberOfPhotosAtEachWall;
    public AlgorithmTypes AlgorithmType;
    public int AlgorithmParameter;
    public GameObject PhotoPrefab;
    public GameObject DebugTextMesh;

    public bool is_running= false;
    public void GenerateLayout(Wall[] walls, Photograph[] photos)
    {   
        var debug = GameObject.Find("DebugBox2").GetComponent<TextMeshPro>();
        int line = 1;
        try
        {

            debug.text = "Just Generating Layout";

            ILayoutAlgorithm algo;
            if (AlgorithmType == AlgorithmTypes.CircularLayout)
            {
                algo = new LayoutAlgorithm();
            }
            else 
            {
                algo = new NoOverlapRandomLayoutAlgorithm();
            }

            
        line = line + 1;
        debug.text = "Generating Layout";

        int numPhotos = 0;
        Layout[] layouts = new Layout[walls.Length];
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos ,walls[i] );
            debug.text = "Wall " + i + " of " + walls.Length + " done";
            numPhotos += layouts[i].photos.Length;
        }
        Debug.Log("Number of photos: " + numPhotos);
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length + "drawing";
            if (layouts[i] != null){
                layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i], PhotoPrefab);
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

    public IEnumerator GenerateLayoutAsync(Wall[] walls, Photograph[] photos)
    {
        is_running = true;
        var debug = GameObject.Find("DebugBox2").GetComponent<TextMeshPro>();
        int line = 1;
        
        debug.text = "Just Generating Layout";

        ILayoutAlgorithm algo;
        if (AlgorithmType == AlgorithmTypes.CircularLayout)
        {
            algo = new LayoutAlgorithm();
        }
        else
        {
            algo = new NoOverlapRandomLayoutAlgorithm();
        }

        line = line + 1;
        debug.text = "Generating Layout";
        int numPhotos = 0;
        Layout[] layouts = new Layout[walls.Length];
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos, walls[i]);
            debug.text = "Wall " + i + " of " + walls.Length + " done";
            numPhotos += layouts[i].photos.Length;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Number of photos: " + numPhotos);
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length + "drawing";
            if (layouts[i] != null)
            {
                layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i], PhotoPrefab);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
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
        is_running = false;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
