using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        Wall[] detected_walls = new Wall[3]; // ToDo: Get walls from the camera
        detected_walls[0] = new Wall();  // ToDo: fill in correct parameters
        detected_walls[1] = new Wall();  // ToDo: fill in correct parameters
        detected_walls[2] = new Wall();  // ToDo: fill in correct parameters

        Photograph[] photos = new Photograph[4]; // ToDo: Get photos
        photos[0] = new Photograph(); // ToDo: fill in correct parameters
        photos[1] = new Photograph(); // ToDo: fill in correct parameters
        photos[2] = new Photograph(); // ToDo: fill in correct parameters
        photos[3] = new Photograph(); // ToDo: fill in correct parameters

        PhotoWallGenerator generator = new PhotoWallGenerator(detected_walls, photos, "NoOverlapRandom");
        generator.GenerateLayout();
    }

    void Update()
    {
        
    }
}