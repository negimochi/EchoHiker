using UnityEngine;
using System.Collections;

[System.Serializable]
public class Point
{
    public Rect rect;
    public Vector2 vec;

    public Point( float minX, float minY, float maxX, float maxY )
    {
        vec = new Vector2();
        rect = new Rect(minX, minY, (maxX-minX), (maxY-minY));
    }

    public void Polar(float r, float theta)
    {
        vec.x = r * Mathf.Cos(theta);
        vec.y = r * Mathf.Sin(theta);
        if (!rect.Contains(vec))
        {
            if (vec.x < rect.xMin) vec.x = rect.xMin;
            else if (vec.x > rect.xMax) vec.x = rect.xMax;
            if (vec.y < rect.yMin) vec.y = rect.yMin;
            else if (vec.y > rect.yMax) vec.y = rect.yMax;
        }
    }

    public void Cartesian(float x_, float y_)
    {
        vec.x = x_;
        vec.y = y_;
        if (!rect.Contains(vec))
        {
            if (vec.x < rect.xMin) vec.x = rect.xMin;
            else if (vec.x > rect.xMax) vec.x = rect.xMax;
            if (vec.y < rect.yMin) vec.y = rect.yMin;
            else if (vec.y > rect.yMax) vec.y = rect.yMax;
        }
    }

    public float GetR()
    {
        return vec.magnitude; 
    }
    public void SetR( float value )
    {
        Polar(value, GetAngle());
    }
    public float GetAngle()
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }
    public void SetAngle( float value )
    {
        Polar(GetR(), value);
    }
}
