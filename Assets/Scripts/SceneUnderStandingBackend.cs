using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.WindowsSceneUnderstanding.Experimental;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialObjectMeshObserver;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Unity.VisualScripting;

public class SceneUnderStandingBackend : MonoBehaviour
{
    // Start is called before the first frame update

    string meshObserverNameWind = "Windows Scene Understanding Observer";
    //WindowsSceneUnderstandingObserver coolObserver;
    private WindowsSceneUnderstandingObserver observer;

    private List<GameObject> instantiatedPrefabs;

    //private Dictionary<SpatialAwarenessSurfaceTypes, Dictionary<int, SpatialAwarenessSceneObject>> observedSceneObjects;
    private Dictionary<int, SpatialAwarenessSceneObject> observedWalls;

    void Start()
    {
        IMixedRealityDataProviderAccess dataProviderAccess = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;
        //CoreServices.SpatialAwarenessSystem.ResumeObservers();
        if (dataProviderAccess == null)
            Debug.Log("DataProvider Access is Faulty for scene understanding!");
        else
        {
            // Spatial Mesh Observer Uses the prefab data to display on Unity. I think it won't work on Hololens, I need to check
            observer = dataProviderAccess.GetDataProvider<WindowsSceneUnderstandingObserver>(meshObserverNameWind);
            // Windows Mesh Observer I think is what scans the room in hololens

            if (observer != null)
            {
                Debug.Log($"SceneUnderstanding observer type {observer.GetType()}\n");
                Debug.Log(observer.Name + " is the name \n");
            }
            else
            {
                Debug.Log("Windows Scene Understanding Observer" + " is not found by name \n");
            }
        }
        observedWalls = new Dictionary<int, SpatialAwarenessSceneObject>();


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

            foreach (var CurrentObject in CurrentObjects) 
            {
                if (CurrentObject.Value.SurfaceType == SpatialAwarenessSurfaceTypes.Wall)
                {

                    var wall = CurrentObject.Value;

                    if (wall == null)
                    { Debug.Log($"Our wall {CurrentObject.Key} is null!"); continue; }
                    observedWalls.Add(CurrentObject.Key, wall);
                    Debug.Log($"Position {wall.Position}, Rotation {wall.Rotation}  ");
                        
                }
            }
            Debug.Log($"Detected Wall Count: {observedWalls.Count}");
        }

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
