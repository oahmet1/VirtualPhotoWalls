using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algorithms 
{
    // Start is called before the first frame update
    
    int numPhotos;
    int numPhotos;
    int numPhotos;
    int numPhotos;
    int numPhotos;

    public algorithms(int numPhotos, int height, int width, int positionX,int positionY)
    {
        this.numPhotos = numPhotos;
    }

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

    Photograph[] sun(ax, num_images=25, radius=0.5):
    # place image in a circle
    prev_img = []
    for image in range(num_images):
        r = radius

        x = -1;
        y = -1;
        valid = False
        while not valid:
            angle = random.random() * 2 * np.pi
            x = r * np.cos(angle) + 0.5
            y = r * np.sin(angle) + 0.5
            violated = False
            for (x_old, y_old) in prev_img:
                if do_circles_intersect((x+0.1/2, y+0.1/2, 0.0707), (x_old+0.1/2, y_old+0.1/2, 0.0707)) or not is_inside_plot(x, y):
                    violated = True
                    break
            if not violated:
                valid = True
        add_rect(ax, x, y, color='red', angle=angle)
        prev_img.append((x, y))
    

    // Update is called once per frame
    
}
