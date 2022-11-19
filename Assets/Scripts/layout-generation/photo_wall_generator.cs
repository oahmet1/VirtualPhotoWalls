using UnityEngine;
using game_objects.Photograph;
using game_objects.Wall;
using algorithms;

public class PhotoWallGenerator : MonoBehaviour
{
    void Start(){
        Wall wall = new Wall(0, 0, 0, 10, 10);
        Photograph[] photos = new Photograph[10];
        algorithms.RandomLayoutAlgorithm algo = new algorithms.RandomLayoutAlgorithm();
        algo.GenerateLayout(photos, wall);
        for (int i = 0; i < photos.Length; i++)
        {
            if (photos[i].displayed) {
                photos[i].Draw();
            }
        }
    }
}