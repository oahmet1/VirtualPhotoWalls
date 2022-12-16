using System.IO;
using System.Drawing;
using System.Linq;
using UnityEngine;
using TMPro;
using System;


//Windows.Storage.KnownFolderId.Pict
//using Win
public class mymain : MonoBehaviour
{
    private Wall[] walls;
    public mymain(Wall[] walls)
    {
        this.walls = walls;
    }

  
    public async void NoStart(GameObject DebugTextMesh)
    {

        /*Wall[] detected_walls = new Wall[3]; // ToDo: Get walls from the camera
        detected_walls[0] = new Wall(new float[] {0f,0f,0f}, new float[]{0f,0f,0f}, 10f, 10f);  // ToDo: fill in correct parameters
        detected_walls[1] = new Wall(new float[] {10f,10f,10f}, new float[]{10f,10f,10f}, 10f, 30f);  // ToDo: fill in correct parameters
        detected_walls[2] = new Wall(new float[] {20f,20f,20f}, new float[]{20f,20f,20f}, 30f, 10f);  // ToDo: fill in correct parameters */
        DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"IN NO STArt!";

#if (ENABLE_WINMD_SUPPORT || UNITY_WINRT || UNITY_WINRT_10_0) && !UNITY_EDITOR
        //Windows.Storage.KnownFolders.PicturesLibrary.CreateFolderQuery()
        DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"iN IFFF!";
        
        string path;
        try 
        {
        path = Windows.Storage.KnownFolders.PicturesLibrary.Path;
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"{ex}";
        }
        
        //DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"asdlksdkfsdjhf";
        var p = await Windows.Storage.KnownFolders.PicturesLibrary.GetFilesAsync();
        DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"{p.Count}";

        path = "";
        //string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg") || s.EndsWith(".bmp") || s.EndsWith(".tiff")).ToArray();
        Windows.Storage.StorageFile[] files = p.Where(s => s.Path.EndsWith(".jpg") || s.Path.EndsWith(".png") || s.Path.EndsWith(".jpeg") || s.Path.EndsWith(".bmp") || s.Path.EndsWith(".tiff")).ToArray();
        DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"{files.Length}\n{files[0].Path}";
        Photograph[] photos = new Photograph[files.Length];
        for (int i = 0; i < files.Length; i++)
        {   
                    
                //FileInfo file = new FileInfo(files[i].Path);
                //Debug.Log(files[i]);
                // var sizeInBytes = file.Length;
            try 
            {   
                
                //FileStream stream = new FileStream( files[i].Path.Replace(@"\", @"\\"), FileMode.Open, FileAccess.Read, FileShare.Read,
                //bufferSize: 4096, useAsync: true);
                var stream_op = await files[i].OpenReadAsync();
                var stream =  stream_op.AsStream();
                
                DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"Opend filestream";
                // Bitmap img = LoadBitmap(files[i].Path);

                Bitmap img = new Bitmap(stream);
                 //BitmapImage img = new BitmapImage();
                 //img.StreamSource = stream;

                float width = img.Width;
                float height = img.Height;
                img.Dispose();
           
                photos[i] = new Photograph(width, height, files[i].Path);

            DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"Opened BMAP";
            }
            catch (Exception ex)
            {
                DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"{files[i].Path} CANNOT BE OPENED.\nEX:\n{ex}";
            }

             
        }
               
#else

        string path = "Assets/Images";
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
#endif
        // read all phtographs from the folder


        // read all phtographs from the folder
        //string path = "Assets/Images/";

        PhotoWallGenerator generator = new PhotoWallGenerator(this.walls, photos, "NoOverlapRandom");
        generator.GenerateLayout();
    }
    public static Bitmap LoadBitmap(string path)
    {
        //Open file in read only mode
        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        //Get a binary reader for the file stream
        using (BinaryReader reader = new BinaryReader(stream))
        {
            //copy the content of the file into a memory stream
            var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
            //make a new Bitmap object the owner of the MemoryStream
            return new Bitmap(memoryStream);
        }
    }

    void Update()
    {
        
    }
}
