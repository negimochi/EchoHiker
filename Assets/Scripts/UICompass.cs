using UnityEngine;
using System.Collections;

public class UICompass : MonoBehaviour {

    [SerializeField]
    private Texture guiCompass;

    private Matrix4x4 tmpMat;
    private float angleY;

    void Start () {
        angleY = 0.0f;
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
            Vector2 pivotPoint = new Vector2(Screen.width * 0.5f, (float)Screen.height);
//            float angleY = transform.localEulerAngles.y;
            GUIUtility.RotateAroundPivot(angleY, pivotPoint);
            GUI.DrawTexture(new Rect(Screen.width * 0.5f - guiCompass.width * 0.5f,
                                        Screen.height - guiCompass.height * 0.5f,
                                        (float)guiCompass.width,
                                        (float)guiCompass.height), guiCompass);
        }
        GUI.matrix = tmpMat;    // 戻す
    }
}
