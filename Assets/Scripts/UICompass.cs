using UnityEngine;
using System.Collections;

public class UICompass : MonoBehaviour {

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
//        float w = guiCompass.width;
//        float h = guiCompass.height;
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
        // ƒeƒNƒXƒ`ƒƒ‚Ì‰ñ“]‚ÍGUIUtility.RotateAroundPivot‚Å‚Í‚È‚¢‚Æ‰ñ“]‚Å‚«‚È‚¢
        tmpMat = GUI.matrix;    // ˆêŽž‘Þ”ð
        {
            GUIUtility.RotateAroundPivot(angleY, pivotPoint);
            GUI.DrawTexture(textureRect, guiCompass);
        }
        GUI.matrix = tmpMat;    // –ß‚·
    }
}
