using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algo : MonoBehaviour
{
    // Start is called before the first frame update
     // class that returns an array of photos to be placed on a wall
    public Photograph[] placePhotos(int numPhotos = 4)
    {
        // create an array of photos
        Photograph[] photos = new Photograph[numPhotos];
        // create a random number generator
        System.Random rnd = new System.Random();

        // create a random number of photos
        for (int i = 0; i < numPhotos; i++)
        {
            // create a random x, y, and z coordinate
            float x = rnd.Next(-5, 5);
            float y = rnd.Next(-5, 5);
            float z = rnd.Next(-5, 5);
            // create a new photo at the random coordinates
            photos[i] = new Photograph(x, y, z);
        }
        return photos;
    }

    void Start(){
        // create a wall
        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.localScale = new Vector3(10, 10, 1);
        wall.transform.position = new Vector3(0, 0, 0);
        // create a random number of photos
        Photograph[] photos = placePhotos();
        // place the photos on the wall
        for (int i = 0; i < photos.Length; i++)
        {
            photos[i].Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
