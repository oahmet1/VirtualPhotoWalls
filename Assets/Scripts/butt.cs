using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;


public class butt : MonoBehaviour
{
    bool is_working = false;
    //var spatialAwarenessService = CoreServices.SpatialAwarenessSystem;
    // Cast to the IMixedRealityDataProviderAccess to get access to the data providers
    string meshObserverName = "Spatial Object Mesh Observer";
    string meshObserverNameWind = "XR SDK Windows Mixed Reality Spatial Mesh Observer";
    IMixedRealitySpatialAwarenessMeshObserver spatialObjectMeshObserver;
    IMixedRealitySpatialAwarenessMeshObserver windowsMeshObserver ;

    // Start is called before the first frame update
    void Start()

    {
        IMixedRealityDataProviderAccess dataProviderAccess = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;
        //CoreServices.SpatialAwarenessSystem.ResumeObservers();
        if (dataProviderAccess == null)
            Debug.Log("DataProvider Access is Faulty!");
        else 
        {   
            // Spatial Mesh Observer Uses the prefab data to display on Unity. I think it won't work on Hololens, I need to check
            spatialObjectMeshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>(meshObserverName);
            // Windows Mesh Observer I think is what scans the room in hololens
            windowsMeshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>(meshObserverNameWind);
        }

        if (spatialObjectMeshObserver == null)
            Debug.Log("Spatial Object Mesh Observer is not set.");

        if (windowsMeshObserver == null)
            Debug.Log("Windowst Mesh Observer is not set.");
        else
        {
            Debug.Log($"Spat type {spatialObjectMeshObserver.GetType()}\n"  );
            Debug.Log($"windowsMeshObserver type {spatialObjectMeshObserver.GetType()}\n");
            windowsMeshObserver.Suspend();
            Debug.Log("Windows Mesh Observer Suspend is correctly called!");
            //spatialObjectMeshObserver = windowsMeshObserver;
        }
        //CoreServices.SpatialAwarenessSystem.ResumeObservers();
        //var meshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        //Debug.Log("HIIIIIII");
        //Debug.Log(spatialObjectMeshObserver.Name);

        // CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }
    protected void StartScan() {
        // Debug.Log(spatialObjectMeshObserver.Name + "Resumed\n");
        //CoreServices.SpatialAwarenessSystem.ResumeObservers();
        spatialObjectMeshObserver.Resume();
        Debug.Log(spatialObjectMeshObserver.Name + "Resumed\n");
        spatialObjectMeshObserver.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
    }

    protected void StopScan() {
        //CoreServices.SpatialAwarenessSystem.SuspendObservers();
        spatialObjectMeshObserver.Suspend();
        //spatialObjectMeshObserver.
        Debug.Log(spatialObjectMeshObserver.Name + "Suspended\n");
        spatialObjectMeshObserver.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
    }

    public void ToogleScan()
    {
        if (is_working) { StopScan(); is_working = false;}
        else { StartScan(); is_working = true; }   
        return;

    }


}
