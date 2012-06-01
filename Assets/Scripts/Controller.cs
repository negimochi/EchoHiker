using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 3.0f;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private ForceMode forcemode = ForceMode.Force;
    [SerializeField]
    private Texture guiCompass;

    private Matrix4x4 matrix;

    private float velocity;

	void Start () 
    {
        GameObject gameObj = GameObject.Find("/UI/Sonar");
        if (gameObj) { 
            GUITexture guiSonar = gameObj.GetComponent<GUITexture>();
            guiSonar.pixelInset = new Rect(20, Screen.height - 260, 240, 240);
        }
        velocity = 1.0f;
    }
	
	void FixedUpdate () 
    {
        // ドラッグ開始
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseButtonDown");
        }
        // ドラッグ中
        if (Input.GetMouseButton(0) ) {
            Debug.Log("MouseButton");

//            velocity = Mathf.Clamp(velocity + Input.GetAxis("Mouse Y") * speed, 0.0f, maxSpeed);
            rigidbody.AddTorque(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime, 0, forcemode);
        }
        // ドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MouseButtonUp");
        }

        // 前に進む
        // んーForceじゃ厳しいかも・・・
        transform.Translate(transform.forward * velocity * Time.fixedDeltaTime);
//        rigidbody.AddForce(transform.forward * velocity * Time.fixedDeltaTime, forcemode);
        if (Input.touchCount > 0)
        {
//            if (guiTexture.HitTest())
            {

            }
        }

//        if( Input.GetAxis("Fire1") ) {
  //          ;
    //    }
	}

    void Rotate(float movement) 
    {
    }

    void OnGUI()
    {
        // テクスチャの回転はGUIUtility.RotateAroundPivotではないと回転できない

        matrix = GUI.matrix;    // 一時退避
        {
            Vector2 pivotPoint = new Vector2(Screen.width * 0.5f, (float)Screen.height);
            float angleY = transform.localEulerAngles.y;
            GUIUtility.RotateAroundPivot(angleY, pivotPoint);
            GUI.DrawTexture(new Rect(   Screen.width * 0.5f - guiCompass.width * 0.5f, 
                                        Screen.height - guiCompass.height * 0.5f, 
                                        (float)guiCompass.width, 
                                        (float)guiCompass.height), guiCompass);
        }
        GUI.matrix = matrix;    // 戻す
    }
}
