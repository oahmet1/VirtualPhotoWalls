using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.SceneUnderstanding;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;

public class SceneUnderstandingHandler: MonoBehaviour
{
    private IMixedRealitySceneUnderstandingObserver observer;
    private Dictionary<int, SpatialAwarenessSceneObject> observedWalls;
    //private List<GameObject> instantiatedPrefabs;

    public GameObject text;
    private int count = 0;
    private bool is_scanning;
    public List<UnityEngine.Vector3> wall_centers;
    public List<UnityEngine.Quaternion> wall_rotations;
    public List<UnityEngine.Vector3> wall_extents;


    protected void Start()
    {

        observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySceneUnderstandingObserver>();
       
        if (observer == null)
        {
            Debug.LogError("Couldn't access Scene Understanding Observer! Please make sure the current build target is set to Universal Windows Platform. "
                + "Visit https://docs.microsoft.com/windows/mixed-reality/mrtk-unity/features/spatial-awareness/scene-understanding for more information.");
            return;
        }

        observedWalls = new Dictionary<int, SpatialAwarenessSceneObject>();
        wall_centers   = new List<Vector3>();
        wall_rotations = new List<Quaternion>();
        wall_extents   = new List<Vector3>();


        //instantiatedPrefabs = new List<GameObject>();

        is_scanning = true;
        DrawMeshes();
        text.GetComponent<TextMeshProUGUI>().text = $"Started Scanning!";
    }

    protected void Update()
    { 

    }
    Bounds getRenderBounds(GameObject objeto)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Renderer render = objeto.GetComponent<Renderer>();
        if (render != null)
        {
            return render.bounds;
        }
        return bounds;
    }
    Bounds getBounds(GameObject objeto)
    {
        Bounds bounds;
        Renderer childRender;
        bounds = getRenderBounds(objeto);
        if (bounds.extents.x == 0)
        {
            bounds = new Bounds(objeto.transform.position, Vector3.zero);
            foreach (Transform child in objeto.transform)
            {
                childRender = child.GetComponent<Renderer>();
                if (childRender)
                {
                    bounds.Encapsulate(childRender.bounds);
                }
                else
                {
                    bounds.Encapsulate(getBounds(child.gameObject));
                }
            }
        }
        return bounds;
    }

    public void GetCurrentWalls() 
    // Reads the info from the scene understanding observer
    // Saves it in the observedWalls variable
    {
        observer.UpdateInterval = 8.0f;

        var CurrentObjects = observer.SceneObjects;
        //Debug.Log($"Detected Object Count: {CurrentObjects.Count}");
        observedWalls = new Dictionary<int, SpatialAwarenessSceneObject>();

        //Save walls in observedwalls
        foreach (var CurrentObject in CurrentObjects)
        {
            if (CurrentObject.Value.SurfaceType == SpatialAwarenessSurfaceTypes.Wall)
            {
                var wall = CurrentObject.Value;
                if (wall == null) { Debug.Log($"Our wall {CurrentObject.Key} is null!"); continue; }
                observedWalls.Add(CurrentObject.Key, wall);
                // Debug.Log($"Position {wall.Position}, Rotation {wall.Rotation}  ");

            }
        }       
    }

    public void UpdateWallInfo()
    // Updates public variables to be used by the image alignment algorithm
    {
        int count = 0;
        foreach (var wall in observedWalls.Values)
        {
            if (wall == null) continue;

            //Try to get bounds
            var bounds = new Bounds(Vector3.zero, Vector3.zero);
            if (wall.Renderer == null)
            {
                bounds = getBounds(wall.GameObject);
            }
            if (wall.Renderer != null) bounds = wall.Renderer.bounds;

            wall_centers.Add(wall.Position);
            wall_rotations.Add(wall.Rotation);
            wall_extents.Add(bounds.extents);

            string msg = "";
            if (wall.Position != null) msg = msg + $"Position: {wall.Position}\n";
            if (wall.Rotation != null) msg = msg + $"Rotation: {wall.Rotation}\n";
            msg = msg + $"Bounds Extent: {bounds.extents}\n";
            msg = msg + $"Bounds Center: {bounds.center}\n";

            //int quad_count = 0;
            //foreach (var quad in wall.Quads)
            //{   
            //    msg = msg + $"ID: {quad_count} Quad Extent : {quad.Extents}\n";
            //    quad_count++;
            //}

            //Debug.Log(msg);
            //count++;
            //if (count > 5) break;
        }
    }

    public void IncreaseCounter() 
    {
        count++;
        GetCurrentWalls(); 
        text.GetComponent<TextMeshProUGUI>().text = $"Click Count: {count}\n Wall Count {observedWalls.Count}";
    }

    private void ClearMeshes() 
    {
        observer.RequestMeshData = false;
        observer.ClearObservations();
        observer.UpdateOnDemand();
    }

    private void DrawMeshes() 
    {
        observer.UpdateInterval = 3.0f;
        observer.RequestMeshData = true;
        observer.UpdateOnDemand();
    }
    public void UpdateEnvironment()
    {
        var message_string = "";
        count++;                    //Increase Press count
        is_scanning = !is_scanning; //Toggle the scanning state

        if (is_scanning)
        {
            //observer.Enable();
            DrawMeshes();
            message_string = "Scanning";
        }
        else 
        {
            //observer.Disable();
            GetCurrentWalls();
            ClearMeshes();
            UpdateWallInfo();
            message_string = $"Wall Count {observedWalls.Count}";
        }

        
        string debug_wall_info = "";
        if(wall_centers.Count > 0) debug_wall_info =  $"Center: {wall_centers[0]} , Extent: {wall_extents[0]}";
        text.GetComponent<TextMeshProUGUI>().text = $"Click Count: {count}\n{message_string}\n{debug_wall_info}";
    }

}
