using UnityEngine;

public class Photograph
{
    float x, y, z, width, height;

    public Photograph(float width, float height)
    {
        this.width = width;
        this.height = height;
    }
    public Photograph createCopy()
    {
        return new Photograph(width, height);
    }
    
    public void Draw(Transform parent){
        // ToDo: Apply photograph texture to the rectangle
        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        photo.transform.localScale = new Vector3(width, height, 0.01f);
        photo.transform.localPosition = new Vector3(x, y, z) + parent.localPosition;
        photo.transform.rotation = parent.rotation;
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
    float[] centerCoordinates, rotationAngles;
    float width, height;
    public Wall(float[] centerCoordinates, float[] rotationAngles, float width, float height )
    {
        this.centerCoordinates = centerCoordinates;
        this.rotationAngles = rotationAngles;
        this.width = width;
        this.height = height;
    }
    
    public Transform Draw(){
        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.localScale = new Vector3(this.width, this.height, 0.1f);
        wall.transform.position = new Vector3(this.centerCoordinates[0], this.centerCoordinates[1], this.centerCoordinates[2]);
        //photo.transform.rotation = new Vector3((float)0, (float)1, (float)0);
        wall.transform.Rotate(this.rotationAngles[0], this.rotationAngles[1], this.rotationAngles[2]);
        return wall.transform;
    }

    public float[] GetBoundsX(){
        return new float[]{-this.width/2, this.width/2};
    }

    public float[] GetBoundsY(){
        return new float[]{-this.height/2, this.height/2};
    }



}