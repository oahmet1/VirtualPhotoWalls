using UnityEngine;
using System.IO;
using TMPro;
using System;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;


public class Photograph
{
    public float x, y, z, width, height, aspectRatio;
    string texturePath;
    Wall wall;
    byte[] bytes;
    public GameObject photo;

     public TextAsset imageAsset;

    public Photograph(float width, float height, byte[] bytes)
    {
        this.aspectRatio = width / height;
        this.height = 0.25f;
        this.width = this.aspectRatio * this.height;
        this.bytes = bytes;
        
    }


    public Photograph(float width, float height, float x, float y, float z, byte[] bytes)
    {
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        this.z = z;
        this.bytes = bytes;
    }

    public Photograph createCopy()
    {
        return new Photograph(width, height, x, y, z, bytes);
    }
    
    public void Draw(float[] rotationAngles, float[] centerCoordinates, Transform parent, Wall wall, GameObject PhotoPrefab,
    Material bboxMaterial, Material bboxMaterialGrabbed, Material bboxHandleWhite, Material bboxHandleBlueGrabbed, GameObject bboxHandlePrefab, GameObject bboxHandleSlatePrefab, GameObject bboxHandleRotationPrefab){
        
        
        var debug = GameObject.Find("DebugBox").GetComponent<TextMeshPro>();
        debug.text = "Drawing photo";
        int line = 1;

        try{

        // ToDo: Apply photograph texture to the rectangle
        //this.textmesh.GetComponent<TextMesh>().text = "Starting to draw photo";
            

        //var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var photo = GameObject.Instantiate(PhotoPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        BoundsControl boundsControl = photo.GetComponent<BoundsControl>();
        BoxDisplayConfiguration boxConfiguration = boundsControl.BoxDisplayConfig;
        boxConfiguration.BoxMaterial = bboxMaterial;
        boxConfiguration.BoxGrabbedMaterial = bboxMaterialGrabbed;
        ScaleHandlesConfiguration scaleHandleConfiguration = boundsControl.ScaleHandlesConfig;
        scaleHandleConfiguration.HandleMaterial = bboxHandleWhite;
        scaleHandleConfiguration.HandleGrabbedMaterial = bboxHandleBlueGrabbed;
        scaleHandleConfiguration.HandlePrefab = bboxHandlePrefab;
        scaleHandleConfiguration.HandleSlatePrefab = bboxHandleSlatePrefab;
        scaleHandleConfiguration.HandleSize = 0.016f;
        scaleHandleConfiguration.ColliderPadding = Vector3.one * 0.016f;
        RotationHandlesConfiguration rotationHandleConfiguration = boundsControl.RotationHandlesConfig;
        rotationHandleConfiguration.HandleMaterial = bboxHandleWhite;
        rotationHandleConfiguration.HandleGrabbedMaterial = bboxHandleBlueGrabbed;
        rotationHandleConfiguration.HandlePrefab = bboxHandleRotationPrefab;
        rotationHandleConfiguration.HandleSize = 0.016f;
        rotationHandleConfiguration.ColliderPadding = Vector3.one * 0.016f;
        rotationHandleConfiguration.ShowHandleForX = false;
        rotationHandleConfiguration.ShowHandleForY = false;
        rotationHandleConfiguration.ShowHandleForZ = false;
        ProximityEffectConfiguration proximityEffectConfiguration = boundsControl.HandleProximityEffectConfig;
        proximityEffectConfiguration.ProximityEffectActive = true;

        //// set up proper bounding boxes
        //BoundsControl bbox = photo.AddComponent<BoundsControl>();
        //bbox.BoundsControlActivation = Microsoft.MixedReality.Toolkit.UI.BoundsControlTypes.BoundsControlActivationType.ActivateByProximityAndPointer;
        //bbox.ScaleHandlesConfig.HandleSize = 0.05f;

            line = line + 1; // 2
       // this.textmesh.GetComponent<TextMeshPro>().text = "Created the cube";

        photo.transform.localScale = new Vector3(this.width/wall.width, this.height/wall.height, 0.05f);
        //line = line + 1; // 3
        ////photo.transform.Rotate(rotationAngles[0], rotationAngles[1], rotationAngles[2]);
            photo.transform.parent = parent;
            line = line + 1; // 4
            photo.transform.position += new Vector3(x / wall.width, y / wall.height, 1f);
            line = line + 1; // 5

            //this.textmesh.GetComponent<TextMeshPro>().text = "Scaled the cube";

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

        
        //Material imageMaterial =new Material(Shader.Find("Standard"));
        //line = line + 1; // 12
        //imageMaterial.SetTexture("_MainTex", texture);
        //line = line + 1; // 13
        
        Renderer imageRenderer =photo.GetComponent<Renderer>();
        line = line + 1;
        //imageRenderer.material = imageMaterial;
        imageRenderer.material.SetTexture("_MainTex", texture);
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

    public float[] GetPosition(){
        return new float[]{this.x, this.y, this.z};
    }

    public bool IsOverlapping(Photograph photo){
        return this.IsOverlappingWithMargin(photo, 0);
    }
    public bool IsOverlappingWithMargin(Photograph photo, float margin){
        float distanceInXDirection = Math.Abs(this.x - photo.x);
        float distanceInYDirection = Math.Abs(this.y - photo.y);
        if (distanceInXDirection < (this.width + photo.width)/2 + margin && distanceInYDirection < (this.height + photo.height)/2 + margin){
            Debug.Log($"Photos IS overlapping: TX {this.x} TY {this.y} W {this.width} H {this.height} PX {photo.x} PY {photo.y} PW {photo.width} PH {photo.height}");
            return true;
        }
        Debug.Log($"Photos IS NOT overlapping: TX {this.x} TY {this.y} W {this.width} H {this.height} PX {photo.x} PY {photo.y} PW {photo.width} PH {photo.height}");
        return false;
    }
}

public class Wall
{
    public float[] centerCoordinates, rotationAngles;
    public float width, height;
    public Transform transform ;
    public GameObject wall;
    Vector3 mainCameraPos;
    public Wall(float[] centerCoordinates, float[] rotationAngles, float width, float height, Vector3 mainCameraPos)
    {
        this.centerCoordinates = centerCoordinates;
        this.rotationAngles = rotationAngles;
        this.width = width;
        this.height = height;

        this.mainCameraPos = mainCameraPos;
        var wall = new GameObject();
        //wall.transform.parent = sceneContent.transform;
        this.wall = wall;
        this.transform = wall.transform;
        //DebugTextMesh.GetComponent<TextMeshPro>().text = $"width: {width} \n height: {height} \n centerCoordinates: {centerCoordinates[0]}, {centerCoordinates[1]}, {centerCoordinates[2]} \n rotationAngles: {rotationAngles[0]}, {rotationAngles[1]}, {rotationAngles[2]}";

        //Material imageMaterial = new Material(Shader.Find("Standard"));
        //Renderer imageRenderer = wall.GetComponent<Renderer>();
        //imageRenderer.material = imageMaterial;

    }

    public void Draw(){
        //this.textmesh.GetComponent<TextMeshPro>().text = $"It is drawing";
        this.transform.localScale = new Vector3(this.width, this.height, 0.1f);
        this.transform.position = new Vector3(this.centerCoordinates[0], this.centerCoordinates[1], this.centerCoordinates[2]);
        //photo.transform.rotation = new Vector3((float)0, (float)1, (float)0);
        this.transform.Rotate(this.rotationAngles[0], this.rotationAngles[1], this.rotationAngles[2]);
        
        float res = Vector3.Dot(wall.transform.position -mainCameraPos, wall.transform.forward);
        if(res > 0){
            wall.transform.Rotate(0, 180, 0);
        }


    }

    public bool photoIsInsideWall(Photograph photo, float[] margin){
        // margin[0] = margin top
        // margin[1] = margin right
        // margin[2] = margin bottom
        // margin[3] = margin left 
        float[] photoPos = photo.GetPosition();
        float photoWidth = photo.width;
        float photoHeight = photo.height;
        float[] wallBoundsX = this.GetBoundsX();
        float[] wallBoundsY = this.GetBoundsY();
        if (photoPos[0] - photoWidth/2 - margin[3] > wallBoundsX[0] &&
            photoPos[0] + photoWidth/2 + margin[1] < wallBoundsX[1] &&
            photoPos[1] - photoHeight/2 - margin[2] > wallBoundsY[0] &&
            photoPos[1] + photoHeight/2 + margin[0] < wallBoundsY[1]){
            return true;
        }
        return false;
    }

    public float[] GetBoundsX(){
        return new float[]{-this.width/2, this.width/2};
    }

    public float[] GetBoundsY(){
        return new float[]{-this.height/2, this.height/2};
    }

}