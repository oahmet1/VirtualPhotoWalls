using UnityEngine;

public class Photograph
{
    float x;
    float y;
    float z;
    float width;
    float height;
    public bool displayed;
    public Photograph(float x, float y, float z, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.width = width;
        this.height = height;
        this.displayed = false;
    }
    
    public void Draw(){
        // ToDo: Apply photograph texture to the rectangle
        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        photo.transform.localScale = new Vector3(this.width, this.height, (float) 0.01);
        photo.transform.position = new Vector3(this.x, this.y, this.z);
    }

    public void SetPosition(float x, float y, float z){
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public void SetDisplayed(bool b)
    {
        this.displayed=b;
    }

    public bool IsOverlapping(Photograph photo){
        if (this.x + this.width < photo.x || photo.x + photo.width < this.x){
            return false;
        } else if (this.y + this.height < photo.y || photo.y + photo.height < this.y){
            return false;
        } else if (this.z + this.height < photo.z || photo.z + photo.height < this.z){
            return false;
        } else {
            return true;
        }
    }
}

public class Wall
{
    float[] centerCoordinates;
    float[] rotationAngles;
    float width;
    float height;
    public Wall(float[] centerCoordinates, float[] rotationAngles, float width, float height )
    {
        this.centerCoordinates = centerCoordinates;
        this.rotationAngles = rotationAngles;
        this.width = width;
        this.height = height;
    }
    
    public void Draw(){
        var photo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        photo.transform.localScale = new Vector3(this.width, this.height, 0.1f);
        photo.transform.position = new Vector3(this.centerCoordinates[0], this.centerCoordinates[1], this.centerCoordinates[2]);
        //photo.transform.rotation = new Vector3((float)0, (float)1, (float)0);
        photo.transform.Rotate(this.rotationAngles[0], this.rotationAngles[1], this.rotationAngles[2]);
    }

    public float[] GetBoundsX(){
        return new float[] { this.centerCoordinates[0] - this.width/2, this.centerCoordinates[0] + this.width/2};
    }

    public float[] GetBoundsY(){
        return new float[]{this.centerCoordinates[1] - this.height/2, this.centerCoordinates[1] + this.height/2};
    }

    public float[] GetBoundsZ(){
        return new float[]{this.z - this.height/2, this.z + this.height/2};
    }
}