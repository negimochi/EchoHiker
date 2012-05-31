using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private ForceMode forcemode = ForceMode.Force;
    [SerializeField]
    private Texture guiCompass;

    private float velocity;

	void Start () 
    {
        GameObject gameObj = GameObject.Find("/UI/Sonar");
        if (gameObj) { 
            GUITexture guiSonar = gameObj.GetComponent<GUITexture>();
            guiSonar.pixelInset = new Rect(20, Screen.height - 260, 240, 240);
        }
    velocity = speed;
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
//            velocity += Input.GetAxis("Mouse Y") * speed;
            Rotate( Input.GetAxis("Mouse X") );
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
//        transform.Rotate(0, movement * stepY * Time.fixedDeltaTime, 0);
        rigidbody.AddTorque(0, movement * rotationSpeed * Time.fixedDeltaTime, 0, forcemode);
//        rigidbody.MoveRotation(Quaternion.AngleAxis(movement * rotationSpeed * Time.fixedDeltaTime, Vector3.up));
        
//        if (guiCompass)
//        {
//            Debug.Log("guiCompass");
//            guiCompass.transform.Rotate(0, 0, movement * rotationSpeed);
 //       }
    }

    void OnGUI()
    {
        Vector2 pivotPoint = new Vector2(Screen.width / 2, Screen.height);
	    float angleY = transform.localEulerAngles.y;
	    GUIUtility.RotateAroundPivot(angleY, pivotPoint);
        GUI.DrawTexture(new Rect(Screen.width/2-340, Screen.height-340, 680, 680), guiCompass);
    }
}
