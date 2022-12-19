using System.IO;
using System.Drawing;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class FileReader : MonoBehaviour
{

    public async Task<Photograph[]> ReadFiles()
    {
        Photograph[] photos;
#if (ENABLE_WINMD_SUPPORT || UNITY_WINRT || UNITY_WINRT_10_0) && !UNITY_EDITOR
        
      
        var p = await Windows.Storage.KnownFolders.PicturesLibrary.GetFilesAsync();
       
        Windows.Storage.StorageFile[] files = p.Where(s => s.Path.EndsWith(".jpg") || s.Path.EndsWith(".png") || s.Path.EndsWith(".jpeg") || s.Path.EndsWith(".bmp") || s.Path.EndsWith(".tiff")).ToArray();

        photos = new Photograph[files.Length];
        for (int i = 0; i < files.Length; i++)
        {   
               
            int line = 0;
            try 
            {   
                
                var stream_op = await files[i].OpenReadAsync();

                var reader = new Windows.Storage.Streams.DataReader(stream_op.GetInputStreamAt(0));
                var bytes = new byte[stream_op.Size];
                await reader.LoadAsync((uint)stream_op.Size);
                reader.ReadBytes(bytes);
             
                Windows.Storage.FileProperties.ImageProperties imageProperties = await files[i].Properties.GetImagePropertiesAsync();
                
                float width = imageProperties.Width;
                float height = imageProperties.Height;
                         
                photos[i] = new Photograph(width, height, bytes);
            }
            catch (Exception ex)
            {
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
