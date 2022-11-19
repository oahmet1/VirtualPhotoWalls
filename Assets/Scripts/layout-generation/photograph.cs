using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : MonoBehaviour
{
    // Start is called before the first frame update
    public Photograph(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    float x;
    float y;
    float z;
    
    public void Start(){
        // Create a flat rectangle representing a photograph
        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        photo.transform.localScale = new Vector3(1, 1, (float) 0.01);
        photo.transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
