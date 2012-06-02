using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private float moveMaxSpeed = 3.0f;
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private float rotMaxSpeed = 5.0f;
    [SerializeField]
    private float rotSpeed = 1.0f;
    [SerializeField]
    private float attenuationRot = 0.2f;

//    private ForceMode forcemode = ForceMode.Force;

    private Vector3 rotVec;
    private Quaternion deltaRot;

    private UICompass uiCompass;

//    [SerializeField]
//    private Texture guiCompass;
//    private Matrix4x4 tmpMat;

	void Start () 
    {
        GameObject uiObj = GameObject.Find("/UI");
        if (uiObj) { 
            uiCompass = uiObj.GetComponent<UICompass>();
        }
    }
	
	void FixedUpdate () 
    {
        //Debug.Log(rotVec.y);
        if (rotVec.y > 0.0f)
        {
            rotVec.y -= attenuationRot * Time.deltaTime;
            if (rotVec.y < 0.0f) rotVec.y = 0.0f;
        }
        if (rotVec.y < 0.0f)
        {
            rotVec.y += attenuationRot;
            if (rotVec.y > 0.0f) rotVec.y = 0.0f;
        }

        // �h���b�O�J�n
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseButtonDown");
            rotVec = new Vector3(0, Input.GetAxis("Mouse X") * rotSpeed, 0);
            CheckRotSpeed(); 
        }
        // �h���b�O��
        if (Input.GetMouseButton(0) ) 
        {
            Debug.Log("MouseButton" );
            rotVec.y += (Input.GetAxis("Mouse X") * rotSpeed);
            CheckRotSpeed();
        }
        // �h���b�O�I��
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MouseButtonUp");
        }

        // ��]����
        RotatePlayer();
        // �O�ɐi��
        MovePlayer();
	}

    void CheckRotSpeed()
    {
        if (rotVec.y < -rotMaxSpeed) rotVec.y = -rotMaxSpeed;
        if (rotVec.y > rotMaxSpeed)  rotVec.y = rotMaxSpeed;
    }

    void RotatePlayer() 
    {
        //rigidbody.AddTorque(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0, forcemode);
        deltaRot = Quaternion.Euler(rotVec * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
        uiCompass.SetAngle(transform.localEulerAngles.y);
    }

    void MovePlayer()
    {
        //velocity = Mathf.Clamp(velocity + Input.GetAxis("Mouse Y") * speed, 0.0f, maxSpeed);
        // ��[Force���ጵ�������E�E�E
        Vector3 vec = moveSpeed * transform.forward.normalized;
        Debug.Log(transform.forward.normalized);
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        //rigidbody.AddForce(transform.forward * velocity * Time.deltaTime, forcemode);
    }

        /*
    void OnGUI()
    {
        // �e�N�X�`���̉�]��GUIUtility.RotateAroundPivot�ł͂Ȃ��Ɖ�]�ł��Ȃ�
        tmpMat = GUI.matrix;    // �ꎞ�ޔ�
        {
            Vector2 pivotPoint = new Vector2(Screen.width * 0.5f, (float)Screen.height);
            float angleY = transform.localEulerAngles.y;
            GUIUtility.RotateAroundPivot(angleY, pivotPoint);
            GUI.DrawTexture(new Rect(   Screen.width * 0.5f - guiCompass.width * 0.5f, 
                                        Screen.height - guiCompass.height * 0.5f, 
                                        (float)guiCompass.width, 
                                        (float)guiCompass.height), guiCompass);
        }
        GUI.matrix = tmpMat;    // �߂�
    }
        */
}
