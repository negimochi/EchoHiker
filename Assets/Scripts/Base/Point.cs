using UnityEngine;
using System.Collections;

[System.Serializable]
public class Point
{
    public float x { get; set; }
    public float y { get; set; }

    public void Polar(float r, float theta)
    {
        x = r * Mathf.Cos(theta);
        y = r * Mathf.Sin(theta);
    }

    public void Cartesian(float x_, float y_)
    {
        x = x_;
        y = y_;
    }

    public float GetR()
    {
        return new Vector2(x,y).magnitude; 
    }
    public void SetR( float value )
    {
        Polar(value, GetAngle());
    }
    public float GetAngle()
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }
    public void SetAngle( float value )
    {
        Polar(GetR(), value);
    }
}
