using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mymain : MonoBehaviour
{
    void Start()
    {
        Wall[] detected_walls = new Wall[1]; // ToDo: Get walls from the camera
        detected_walls[0] =/* new Wall(new float[] {0f,0f,0f}, new float[]{0f,0f,0f}, 10f, 10f);  // ToDo: fill in correct parameters
        detected_walls[1] = new Wall(new float[] {10f,10f,10f}, new float[]{10f,10f,10f}, 10f, 10f);  // ToDo: fill in correct parameters
        detected_walls[2] =*/ new Wall(new float[] {20f,20f,20f}, new float[]{20f,20f,20f}, 10f, 10f);  // ToDo: fill in correct parameters

        Photograph[] photos = new Photograph[4]; // ToDo: Get photos
        photos[0] = new Photograph(2,2); // ToDo: fill in correct parameters
        photos[1] = new Photograph(2,2); // ToDo: fill in correct parameters
        photos[2] = new Photograph(2,2); // ToDo: fill in correct parameters
        photos[3] = new Photograph(2,2); // ToDo: fill in correct parameters

        PhotoWallGenerator generator = new PhotoWallGenerator(detected_walls, photos, "NoOverlapRandom");
        generator.GenerateLayout();
    }

    void Update()
    {
        
    }
}
