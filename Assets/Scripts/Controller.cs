using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private ForceMode forcemode = ForceMode.Force;

    private float velocity;
    private GUITexture guiCompass;

	void Start () 
    {
        GameObject gameObj = GameObject.Find("/UI/Compass");
        if (gameObj) { 
            guiCompass = gameObj.GetComponent<GUITexture>();
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
        rigidbody.AddForce(transform.forward * velocity * Time.fixedDeltaTime, forcemode);
//        rigidbody.MovePosition(transform.forward * velocity * Time.fixedDeltaTime);
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
//            guiCompass..transform.Rotate(0, 0, movement * stepY);
//        }
    }
}
