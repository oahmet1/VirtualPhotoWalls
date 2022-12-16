using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.WindowsSceneUnderstanding.Experimental;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using TMPro;

public class SceneUnderStandingBackend : MonoBehaviour
{
    // Start is called before the first frame update

    string meshObserverNameWind = "Windows Scene Understanding Observer";
    //WindowsSceneUnderstandingObserver coolObserver;
    private WindowsSceneUnderstandingObserver observer;

    
    //string displayObserverName = "OpenXR Spatial Mesh Observer";
    //IMixedRealitySpatialAwarenessMeshObserver displayObserver;

    private List<GameObject> instantiatedPrefabs;

    //private Dictionary<SpatialAwarenessSurfaceTypes, Dictionary<int, SpatialAwarenessSceneObject>> observedSceneObjects;
    private Dictionary<int, SpatialAwarenessSceneObject> observedWalls;

    void Start()
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
        IMixedRealityDataProviderAccess dataProviderAccess = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;
        //CoreServices.SpatialAwarenessSystem.ResumeObservers();
        if (dataProviderAccess == null)
            Debug.Log("DataProvider Access is Faulty for scene understanding!");
        else
        {
            // Spatial Mesh Observer Uses the prefab data to display on Unity. I think it won't work on Hololens, I need to check
            observer = dataProviderAccess.GetDataProvider<WindowsSceneUnderstandingObserver>(meshObserverNameWind);
            // Windows Mesh Observer I think is what scans the room in hololens
            //displayObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>(displayObserverName);

            if (observer != null)
            {
                Debug.Log($"SceneUnderstanding observer type {observer.GetType()}\n");
                Debug.Log(observer.Name + " is the name \n");
                observer.AutoUpdate = true ;
            }
            else
            {
                Debug.Log("Windows Scene Understanding Observer" + " is not found by name \n");
            }
            //if (displayObserver != null)
            //{
            //    Debug.Log($"DisplayObserver type {displayObserver.GetType()}\n");
            //    Debug.Log(displayObserver.Name + " is the name \n");
            //}
            //else
            //{
            //    Debug.Log("Display Observer \n");
            //}
        }
        observedWalls = new Dictionary<int, SpatialAwarenessSceneObject>();
        //observer.Disable();
        //displayObserver.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
        //displayObserver.Suspend();

        var debugTextMesh = gameObject.GetNamedChild("DebugTextMesh");
        if (observer.IsInitialized) debugTextMesh.GetComponent<TextMeshPro>().text = $"Correctly Initialized.";
        else debugTextMesh.GetComponent<TextMeshPro>().text = $"NOT Correctly Initialized.";



        // var debugTextMesh = gameObject.GetNamedChild ("DebugTextMesh");
        // debugTextMesh.SetActive(false);
    }

    public void ToogleScan()
    {

        if (observer.IsEnabled) {

            observer.Disable();
            Debug.Log("Observer Suspended!");
        }
        else
        {
            observer.Enable();
            Debug.Log("Observer Resumed!");
        }

        if (!observer.IsEnabled)
        {
            //displayObserver.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
            //displayObserver.Suspend();

            var CurrentObjects = observer.SceneObjects;
            Debug.Log($"Detected Object Count: {CurrentObjects.Count}");

            if (observedWalls == null)
            {
                Debug.Log("Our dict is null!");
            }
            else
            {
                observedWalls = new Dictionary<int, SpatialAwarenessSceneObject>();
            }
            ArrayList walls = new ArrayList();
            foreach (var CurrentObject in CurrentObjects)
            {
                if (CurrentObject.Value.SurfaceType == SpatialAwarenessSurfaceTypes.Wall)
                {

                    var wall = CurrentObject.Value;

                    if (wall == null)
                    { Debug.Log($"Our wall {CurrentObject.Key} is null!"); continue; }
                    observedWalls.Add(CurrentObject.Key, wall);
                    Debug.Log($"Position {wall.Position}, Rotation {wall.Rotation}");

                    float[] position = new float[3];
                    position[0] = wall.Position.x;
                    position[1] = wall.Position.y;
                    position[2] = wall.Position.z;
                    float[] rotation = new float[3];
                    rotation[0] = wall.GameObject.transform.eulerAngles.x;
                    rotation[1] = wall.GameObject.transform.eulerAngles.y;
                    rotation[2] = wall.GameObject.transform.eulerAngles.z;

                    float width = wall.Quads[0].GameObject.transform.localScale.x;
                    float height = wall.Quads[0].GameObject.transform.localScale.y;

                   //walls.Add(new Wall(position, rotation, width, height));
                }
            }
            Debug.Log($"Detected Wall Count: {observedWalls.Count}");
            var debugTextMesh = gameObject.GetNamedChild("DebugTextMesh");
            debugTextMesh.GetComponent<TextMeshPro>().text = $"Object Count: {CurrentObjects.Count}";
            // debugTextMesh.SetActive(true);
            Debug.Log($"Walls: {(walls.ToArray(typeof(Wall)) as Wall[]).Length}");
            mymain m = new mymain(walls.ToArray(typeof(Wall)) as Wall[]);
            //m.NoStart();
        }
        else 
        {
            observer.UpdateOnDemand();
            //displayObserver.Resume();
            //displayObserver.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
            var debugTextMesh = gameObject.GetNamedChild("DebugTextMesh");


            var CurrentObjects = observer.SceneObjects;
            Debug.Log($"Detected Object Count: {CurrentObjects.Count}");

            if (observer.IsRunning) debugTextMesh.GetComponent<TextMeshPro>().text = $"Detected Object Count while enabled: {CurrentObjects.Count}";
            else debugTextMesh.GetComponent<TextMeshPro>().text = $"Scanning but not running";
        }

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
