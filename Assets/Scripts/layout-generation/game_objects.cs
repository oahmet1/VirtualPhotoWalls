using UnityEngine;
using System.IO;
using TMPro;
public class Photograph
{
    public float x, y, z, width, height, aspectRatio;
    string texturePath;
    Wall wall;

     public TextAsset imageAsset;

    public Photograph(float width, float height, string texturePath)
    {
        this.aspectRatio = width / height;
        this.height = 0.25f;
        this.width = this.aspectRatio * this.height;
        this.texturePath = texturePath;
    }


    public Photograph(float width, float height, float x, float y, float z, string texturePath)
    {
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        this.z = z;
        this.texturePath = texturePath;

    }

    public Photograph createCopy()
    {
        return new Photograph(width, height, x, y, z, texturePath);
    }
    
    public void Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall){
        // ToDo: Apply photograph texture to the rectangle
        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        photo.transform.localScale = new Vector3(this.width/wall.width, this.height/wall.height, 0.05f);
        //photo.transform.Rotate(rotationAngles[0], rotationAngles[1], rotationAngles[2]);
        photo.transform.parent = parent;
        photo.transform.position += new Vector3(x/wall.width, y/wall.height, 1f);
        Debug.Log("x is :"  + parent.localScale.x);

        Mesh mesh = photo.GetComponent<MeshFilter>().mesh;
        Vector2[] uvs = new Vector2[mesh.vertices.Length];

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
        mesh.uv = uvs;


        byte[] bytes = File.ReadAllBytes(this.texturePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        Material imageMaterial =new Material(Shader.Find("Unlit/Texture"));
        imageMaterial.SetTexture("_MainTex", texture);
        
        Renderer imageRenderer =photo.GetComponent<Renderer>();
        imageRenderer.material = imageMaterial;
    
    }

    public void SetPosition(float x, float y, float z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float[] GetPosition(){
        return new float[]{this.x, this.y, this.z};
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
    public GameObject textmesh;
    public Wall(float[] centerCoordinates, float[] rotationAngles, float width, float height, GameObject sceneContent, GameObject textmesh)
    {
        this.centerCoordinates = centerCoordinates;
        this.rotationAngles = rotationAngles;
        this.width = width;
        this.height = height; 

        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //wall.transform.parent = sceneContent.transform;
        this.wall = wall;
        this.transform = wall.transform;
        this.textmesh = textmesh;
        textmesh.GetComponent<TextMeshProUGUI>().text = $"width: {width} \n height: {height} \n centerCoordinates: {centerCoordinates[0]}, {centerCoordinates[1]}, {centerCoordinates[2]} \n rotationAngles: {rotationAngles[0]}, {rotationAngles[1]}, {rotationAngles[2]}";

    }
    
    public void Draw(){
        textmesh.GetComponent<TextMeshProUGUI>().text = $"It is drawing";
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