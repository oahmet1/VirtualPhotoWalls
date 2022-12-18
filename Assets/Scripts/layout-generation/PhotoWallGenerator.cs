using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEditor;

public class Layout
{
    public Photograph[] photos;

    public Layout(Photograph[] photos)
    {
        this.photos = photos;
    }

    public void  Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall, GameObject PhotoPrefab,
    Material bboxMaterial, Material bboxMaterialGrabbed, Material bboxHandleWhite, Material bboxHandleBlueGrabbed, GameObject bboxHandlePrefab, GameObject bboxHandleSlatePrefab, GameObject bboxHandleRotationPrefab){
        foreach(Photograph photo in photos){
            photo.Draw(rotationAngles, centerCoordinates, parent, wall, PhotoPrefab,
            bboxMaterial, bboxMaterialGrabbed, bboxHandleWhite, bboxHandleBlueGrabbed, bboxHandlePrefab, bboxHandleSlatePrefab, bboxHandleRotationPrefab);
        }
    }
    public void Clear()
    {
        foreach (Photograph photo in photos)
        {
            GameObject.Destroy(photo.photo);
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
    
    public Material bboxMaterial;
    public Material bboxMaterialGrabbed;
    public Material bboxHandleWhite;
    public Material bboxHandleBlueGrabbed;
    public GameObject bboxHandlePrefab;
    public GameObject bboxHandleSlatePrefab;
    public GameObject bboxHandleRotationPrefab;
    Layout[] layouts;

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
        layouts = new Layout[walls.Length];
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length;
            layouts[i] = algo.GenerateLayout(photos ,walls[i],numberOfPhotosAtEachWall, AlgorithmParameter );
            debug.text = "Wall " + i + " of " + walls.Length + " done";
            numPhotos += layouts[i].photos.Length;
        }
        Debug.Log("Number of photos: " + numPhotos);
        line = line + 1;
        for (int i = 0; i < walls.Length; i++)
        {
            debug.text = "Wall " + i + " of " + walls.Length + "drawing";
            if (layouts[i] != null){
                layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i], PhotoPrefab,
                    bboxMaterial, bboxMaterialGrabbed, bboxHandleWhite, bboxHandleBlueGrabbed, bboxHandlePrefab, bboxHandleSlatePrefab, bboxHandleRotationPrefab);
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
            layouts[i] = algo.GenerateLayout(photos, walls[i], numberOfPhotosAtEachWall, AlgorithmParameter);
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
                layouts[i].Draw(walls[i].rotationAngles, walls[i].centerCoordinates, walls[i].transform, walls[i], PhotoPrefab,
                    bboxMaterial, bboxMaterialGrabbed, bboxHandleWhite, bboxHandleBlueGrabbed, bboxHandlePrefab, bboxHandleSlatePrefab, bboxHandleRotationPrefab);
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
    void ClearObjects() 
    {
        foreach (var layout in this.layouts) 
        {
            layout.Clear();
        }
    }
}
