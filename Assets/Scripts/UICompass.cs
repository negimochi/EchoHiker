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
        // �e�N�X�`���̉�]��GUIUtility.RotateAroundPivot�ł͂Ȃ��Ɖ�]�ł��Ȃ�
        tmpMat = GUI.matrix;    // �ꎞ�ޔ�
        {
            Vector2 pivot = new Vector2(Screen.width * 0.5f, (float)Screen.height);
//            float angleY = transform.localEulerAngles.y;
            GUIUtility.RotateAroundPivot(angleY, pivot);
            GUI.DrawTexture(new Rect(Screen.width * 0.5f - guiCompass.width * 0.5f,
                                        Screen.height - guiCompass.height * 0.5f,
                                        (float)guiCompass.width,
                                        (float)guiCompass.height), guiCompass);
        }
        GUI.matrix = tmpMat;    // �߂�
    }
}
