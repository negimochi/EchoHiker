using UnityEngine;
using System.Collections;

[System.Serializable]
public class Point
{
    [SerializeField]
    private Vector2 rect;

    public Point()
    {
        rect = new Vector2();
    }
    public Point(float x, float y)
    {
        rect = new Vector2( x, y );
    }

    public void Polar(float r, float theta)
    {
        rect.x = Mathf.Cos(theta);
        rect.y = Mathf.Sin(theta);
        rect *= r;
    }

    public void Rect(float x, float y)
    {
        rect.x = x;
        rect.y = y;
    }

    public float x
    {
        get { return rect.x; }
        set { rect.x = value; }
    }
    public float z
    {
        get { return this.rect.y; }
        set { rect.y = value; }
    }

    public float r
    {
        get { return rect.magnitude; }
        set { Polar(value, Mathf.Atan2(rect.y, rect.x)); }
    }
    public float theta
    {
        get { return Mathf.Atan2(rect.y, rect.x); }
        set { Polar(rect.magnitude, value); }
    }
}
