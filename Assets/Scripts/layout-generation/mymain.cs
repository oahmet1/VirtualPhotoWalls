using System.IO;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEditor.PackageManager.UI;


public class mymain : MonoBehaviour
{
    private Wall[] walls;
    public mymain(Wall[] walls)
    {
        this.walls = walls;
    }

  
    public void NoStart()
    {
        /*Wall[] detected_walls = new Wall[3]; // ToDo: Get walls from the camera
        detected_walls[0] = new Wall(new float[] {0f,0f,0f}, new float[]{0f,0f,0f}, 10f, 10f);  // ToDo: fill in correct parameters
        detected_walls[1] = new Wall(new float[] {10f,10f,10f}, new float[]{10f,10f,10f}, 10f, 30f);  // ToDo: fill in correct parameters
        detected_walls[2] = new Wall(new float[] {20f,20f,20f}, new float[]{20f,20f,20f}, 30f, 10f);  // ToDo: fill in correct parameters
*/
#if ENABLE_WINMD_SUPPORT
        //Windows.Storage.KnownFolders.PicturesLibrary.CreateFolderQuery()
        string path = Windows.Storage.KnownFolders.PicturesLibrary.Path;
               
#else
        string path = "Assets/Images";
#endif
        // read all phtographs from the folder
        string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg") || s.EndsWith(".bmp") || s.EndsWith(".tiff")).ToArray();
        //string[] files = System.IO.Directory.GetFiles(path, "*.jpeg");

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

        PhotoWallGenerator generator = new PhotoWallGenerator(this.walls, photos, "NoOverlapRandom");
        generator.GenerateLayout();
    }

    void Update()
    {
        
    }
}
