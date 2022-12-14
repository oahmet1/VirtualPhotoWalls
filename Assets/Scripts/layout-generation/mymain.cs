using System.IO;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class mymain : MonoBehaviour
{
    private Wall[] walls;
    public mymain(Wall[] walls)
    {
        this.walls = walls;
    }

    public void NoStart()
    {

        // read all phtographs from the folder
       /* string path = "Assets/Images/";
        string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg") || s.EndsWith(".bmp") || s.EndsWith(".tiff")).ToArray();
        string[] files = System.IO.Directory.GetFiles(path, "*.jpeg");

        Photograph[] photos = new Photograph[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            //Debug.Log(files[i]);
            var sizeInBytes = file.Length;
            Bitmap img = new Bitmap(files[i]);
            float width = img.Width;
            float height = img.Height;
            photos[i] = new Photograph(width, height, files[i]);
        }
*/
        string[] files = new string[10];
        Photograph[] photos = new Photograph[10];
        for (int i = 0; i < 10; i++)
        {
            float width = 50;
            float height = 25;
            photos[i] = new Photograph(width, height, files[i]);
        }
        
        PhotoWallGenerator generator = new PhotoWallGenerator(this.walls, photos, "NoOverlapRandom");
        generator.GenerateLayout();
    }

    void Update()
    {
        
    }
}
