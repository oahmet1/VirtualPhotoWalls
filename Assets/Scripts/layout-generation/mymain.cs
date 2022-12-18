using System.IO;
using System.Drawing;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class FileReader : MonoBehaviour
{

    public GameObject textmesh;
    
    public async Task<Photograph[]> ReadFiles(GameObject DebugTextMesh)
    {

        DebugTextMesh.GetComponent<TextMeshPro>().text = $"IN NO STArt!";
        Photograph[] photos;
#if (ENABLE_WINMD_SUPPORT || UNITY_WINRT || UNITY_WINRT_10_0) && !UNITY_EDITOR
        //Windows.Storage.KnownFolders.PicturesLibrary.CreateFolderQuery()
        //DebugTextMesh.GetComponent<TextMeshPro>().text = $"iN IFFF!";
        
        string path;
        try 
        {
        path = Windows.Storage.KnownFolders.PicturesLibrary.Path;
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            DebugTextMesh.GetComponent<TextMeshPro>().text = $"{ex}";
        }
        
        //DebugTextMesh.GetComponent<TextMeshPro>().text = $"asdlksdkfsdjhf";
        var p = await Windows.Storage.KnownFolders.PicturesLibrary.GetFilesAsync();
        DebugTextMesh.GetComponent<TextMeshPro>().text = $"{p.Count}";

        path = "";
        //string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg") || s.EndsWith(".bmp") || s.EndsWith(".tiff")).ToArray();
        Windows.Storage.StorageFile[] files = p.Where(s => s.Path.EndsWith(".jpg") || s.Path.EndsWith(".png") || s.Path.EndsWith(".jpeg") || s.Path.EndsWith(".bmp") || s.Path.EndsWith(".tiff")).ToArray();
        DebugTextMesh.GetComponent<TextMeshPro>().text = $"{files.Length}\n{files[0].Path}";

        photos = new Photograph[files.Length];
        for (int i = 0; i < files.Length; i++)
        {   
                    
                //FileInfo file = new FileInfo(files[i].Path);
                //Debug.Log(files[i]);
                // var sizeInBytes = file.Length;
            int line = 0;
            try 
            {   
                
                //FileStream stream = new FileStream( files[i].Path.Replace(@"\", @"\\"), FileMode.Open, FileAccess.Read, FileShare.Read,
                //bufferSize: 4096, useAsync: true);
                var stream_op = await files[i].OpenReadAsync();
                line = line+1;

                var reader = new Windows.Storage.Streams.DataReader(stream_op.GetInputStreamAt(0));
                line = line+1;
                var bytes = new byte[stream_op.Size];
                line = line+1;
                await reader.LoadAsync((uint)stream_op.Size);
                line = line+1;
                reader.ReadBytes(bytes);
                line = line+1;
                //var stream =  stream_op.AsStream();
                //line = line+1;

                DebugTextMesh.GetComponent<TextMeshPro>().text = $"Opend filestream";
                // Bitmap img = LoadBitmap(files[i].Path);

                //Bitmap img = new Bitmap(stream);
                line = line+1;

                Windows.Storage.FileProperties.ImageProperties imageProperties = await files[i].Properties.GetImagePropertiesAsync();
                line = line+1;
                float width = imageProperties.Width;
                line = line+1;
                float height = imageProperties.Height;
                line = line+1;
                 //BitmapImage img = new BitmapImage();
                 //img.StreamSource = stream;

                //float width = img.Width;
                //float height = img.Height;
                //img.Dispose();
                line = line+1;
           
                photos[i] = new Photograph(width, height, bytes);

                DebugTextMesh.GetComponent<TextMeshPro>().text = $"Opened BMAP with byte length{bytes.Length}";
            }
            catch (Exception ex)
            {
                DebugTextMesh.GetComponent<TextMeshPro>().text = $"I was done with line : {line}, {files[i].Path} CANNOT BE OPENED.\nEX:\n{ex}";
            }
             
        }
               
#else

    string path = "Assets/Images/bmw10_release/bmw10_ims/1";
        string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg") || s.EndsWith(".bmp") || s.EndsWith(".tiff")).ToArray();
        //string[] files = System.IO.Directory.GetFiles(path, "*.jpeg");

        photos = new Photograph[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            //Debug.Log(files[i]);
            var sizeInBytes = file.Length;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(files[i]);
            float width = img.Width;
            float height = img.Height;
            photos[i] = new Photograph(width, height, File.ReadAllBytes(files[i]));
        }
#endif
        return photos;
    }
      
    
}
