using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class StartRoomScan : MonoBehaviour
{
    private void Start()
    {
        // Start is called before the first frame update
        // Get the first Mesh Observer available, generally we have only one registered
        CoreServices.SpatialAwarenessSystem.ResumeObservers();

        // Suspend Mesh Observation from all Observers
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }


 }
