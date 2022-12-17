using UnityEngine;
using System.IO;
using TMPro;
using System;
public class Photograph
{
    public float x, y, z, width, height, aspectRatio;
    string texturePath;
    Wall wall;
    byte[] bytes;

     public TextAsset imageAsset;

    public Photograph(float width, float height, string texturePath, byte[] bytes)
    {
        this.aspectRatio = width / height;
        this.height = 0.25f;
        this.width = this.aspectRatio * this.height;
        this.texturePath = texturePath;
        this.bytes = bytes;
    }


    public Photograph(float width, float height, float x, float y, float z, string texturePath, byte[] bytes)
    {
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        this.z = z;
        this.texturePath = texturePath;
        this.bytes = bytes;
    }

    public Photograph createCopy()
    {
        return new Photograph(width, height, x, y, z, texturePath, bytes);
    }
    
    public void Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall){
        
        
        var debug = GameObject.Find("DebugBox").GetComponent<TextMeshProUGUI>();
        debug.text = "Drawing photo";
        int line = 1;

        try{

        // ToDo: Apply photograph texture to the rectangle
        //this.textmesh.GetComponent<TextMesh>().text = "Starting to draw photo";

        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        line = line + 1; // 2
       // this.textmesh.GetComponent<TextMesh>().text = "Created the cube";

        photo.transform.localScale = new Vector3(this.width/wall.width, this.height/wall.height, 0.05f);
        line = line + 1; // 3
        //photo.transform.Rotate(rotationAngles[0], rotationAngles[1], rotationAngles[2]);
        photo.transform.parent = parent;
        line = line + 1; // 4
        photo.transform.position += new Vector3(x/wall.width, y/wall.height, 1f);
        line = line + 1; // 5

        //this.textmesh.GetComponent<TextMesh>().text = "Scaled the cube";

        Debug.Log("x is :"  + parent.localScale.x);

        Mesh mesh = photo.GetComponent<MeshFilter>().mesh;
        line = line + 1; // 6
        Vector2[] uvs = new Vector2[mesh.vertices.Length];
        line = line + 1; // 7

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
        mesh.uv = uvs;
        line = line + 1; // 8


        byte[] bytes = this.bytes;// File.ReadAllBytes(this.texturePath);
        line = line + 1; // 9
        Texture2D texture = new Texture2D(2, 2);
        line = line + 1; // 10
        texture.LoadImage(bytes);
        line = line + 1; // 11

        Material imageMaterial =new Material(Shader.Find("Unlit/Texture"));
        line = line + 1; // 12
        imageMaterial.SetTexture("_MainTex", texture);
        line = line + 1; // 13
        
        Renderer imageRenderer =photo.GetComponent<Renderer>();
        line = line + 1;
        imageRenderer.material = imageMaterial;
        line = line + 1;

        //textmesh.GetComponent<TextMesh>().text = "Done Drawing photo, assigned material";


       
        debug.text = "Just Generating Layout";
        }
        catch(Exception ex){
            //throw divide by zero exception
            debug.text = "Drawing photo error on line " + line + " " + ex;
        }
    
    }

    public void SetPosition(float x, float y, float z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public bool IsOverlapping(Photograph photo){
        if (this.x + this.width < photo.x || photo.x + photo.width < this.x){
            return false;
        } else if (this.y + this.height < photo.y || photo.y + photo.height < this.y){
            return false;
        } else {
            return true;
        }
    }
}

public class Wall
{
    public float[] centerCoordinates, rotationAngles;
    public float width, height;
    public Transform transform ;
    public GameObject wall;
    public Wall(float[] centerCoordinates, float[] rotationAngles, float width, float height, GameObject sceneContent, GameObject DebugTextMesh)
    {
        this.centerCoordinates = centerCoordinates;
        this.rotationAngles = rotationAngles;
        this.width = width;
        this.height = height; 

        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //wall.transform.parent = sceneContent.transform;
        this.wall = wall;
        this.transform = wall.transform;
        //DebugTextMesh.GetComponent<TextMeshProUGUI>().text = $"width: {width} \n height: {height} \n centerCoordinates: {centerCoordinates[0]}, {centerCoordinates[1]}, {centerCoordinates[2]} \n rotationAngles: {rotationAngles[0]}, {rotationAngles[1]}, {rotationAngles[2]}";

    }
    
    public void Draw(){
        //this.textmesh.GetComponent<TextMeshProUGUI>().text = $"It is drawing";
        this.transform.localScale = new Vector3(this.width, this.height, 0.1f);
        this.transform.position = new Vector3(this.centerCoordinates[0], this.centerCoordinates[1], this.centerCoordinates[2]);
        //photo.transform.rotation = new Vector3((float)0, (float)1, (float)0);
        this.transform.Rotate(this.rotationAngles[0], this.rotationAngles[1], this.rotationAngles[2]);

        Vector3 mainCameraPos = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
        
        float res = Vector3.Dot(wall.transform.position -mainCameraPos, wall.transform.forward);
        if(res > 0){
            wall.transform.Rotate(0, 180, 0);
        }
    }

    public float[] GetBoundsX(){
        return new float[]{-this.width/2, this.width/2};
    }

    public float[] GetBoundsY(){
        return new float[]{-this.height/2, this.height/2};
    }



}