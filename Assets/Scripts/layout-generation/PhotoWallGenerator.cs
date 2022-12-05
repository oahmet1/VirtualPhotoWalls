using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoWallGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Wall wall = new Wall(0, 0, 0, 100, 100);
        Photograph[] photos = new Photograph[10];
        for(int i = 0; i < photos.Length; i++)
        {
            photos[i] = new Photograph(10, 10, 10, 3, 3);
        }
        NoOverlapRandomLayoutAlgorithm algo = new NoOverlapRandomLayoutAlgorithm();
        algo.GenerateLayout(photos, wall);
        for (int i = 0; i < photos.Length; i++)
        {
            if (photos[i].displayed)
            {
                photos[i].Draw();
            }
        }
        wall.Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
