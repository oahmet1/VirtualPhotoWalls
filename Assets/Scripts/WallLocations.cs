//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class WallLocations : UnityEngine.MonoBehaviour
{
    public UnityEngine.GameObject GameObject;
    public int speed = 5;
    public UnityEngine.Vector3 Locations = new(2.0f, 0.1f, 0.3434f);
    public System.Collections.Generic.List<UnityEngine.Vector3> locs = new( new UnityEngine.Vector3[] { new(0.0f, 0.1f,5.0f), new(0.0f, 0.1f, 9.97f) });
    public System.Collections.Generic.List<int> integer_locs = new( new int[] {5,6,7});
    public WallLocations() 
    {
        //GameObject = GameObject.Find("TestingObject");
    
        //ArrayList temp = { new System.Numerics.Vector2(0.0f, 0.1f), new System.Numerics.Vector2(0.0f, 0.1f) }:
        //locs = new System.Numerics.Vector2[] { new System.Numerics.Vector2(0.0f, 0.1f), new System.Numerics.Vector2(0.0f, 0.1f)} ;
        //loc.Add(5);
        //locs = new(new System.Numerics.Vector2[] { new(0.0f, 0.1f), new(0.0f, 0.1f) });
        //var p = GetComponent<>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject = gameObject.GetNamedChild("TestingObject");
        GameObject.SetActive(false);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
