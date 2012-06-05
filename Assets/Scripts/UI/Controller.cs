using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private Texture guiCompass;
    [SerializeField]
    private float aspect = 0.5f;

    private Matrix4x4 tmpMat;
    private float angleY;

    private Rect textureRect;
    private Vector3 pivotPoint;

    void Start () {
        angleY = 0.0f;
        float w = Screen.width * aspect;
        float h = w;
        pivotPoint  = new Vector2(Screen.width * 0.5f, (float)Screen.height);
        textureRect = new Rect(pivotPoint.x - w * 0.5f, pivotPoint.y - h * 0.5f, w, h);
	}

    public void SetAngle( float angle )
    {
        angleY = angle;
    }

    void OnGUI()
    {
        // テクスチャの回転はGUIUtility.RotateAroundPivotではないと回転できない
        tmpMat = GUI.matrix;    // 一時退避
        {
            GUIUtility.RotateAroundPivot(angleY, pivotPoint);
            GUI.DrawTexture(textureRect, guiCompass);
        }
        GUI.matrix = tmpMat;    // 戻す
    }
}
