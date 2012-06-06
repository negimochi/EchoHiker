using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveMaxSpeed = 3.0f;
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private float rotSpeed = 1.0f;
    [SerializeField]
    private float attenuationRot = 0.2f;
    [SerializeField]
    private  float slowdownRot = 0.4f;
    [SerializeField]
    private float blending = 0.8f;
    [SerializeField]
    private float braking = 0.01f;

    private Vector3 rotVec;
    private Quaternion deltaRot;
    private float rotVecStartY;
    private float currentTime;
    private float currentRot;

    private UIController uiCompass;

	void Start () 
    {
        //        Screen.lockCursor = true;
        GameObject uiObj = GameObject.Find("/UI");
        if (uiObj) {
            uiCompass = uiObj.GetComponent<UIController>();
        }
        currentRot = attenuationRot;
        if (braking < 0.0f) braking = -braking;
    }
	
	void FixedUpdate () 
    {

        /*
        if( Input.GetKeyUp(KeyCode.LeftControl) ){
            Screen.lockCursor = !Screen.lockCursor;
        }
         */

        // 回転の減衰
        if ( rotVec.y != 0.0f )
        {
            rotVec.y = Mathf.SmoothStep(rotVecStartY, 0.0f, currentRot * currentTime);
            currentTime += Time.deltaTime;
            Debug.Log("rot=" + currentRot + ", time="+ currentTime + ", Rot=" + rotVec.y);
        }

        // ドラッグ中
        if (Input.GetMouseButton(0))
        {
            Debug.Log("MouseButton :" + Input.GetAxis("Mouse X"));
            SetRot(Input.GetAxis("Mouse X") * rotSpeed);
        }

        // ドラッグ開始
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseButtonDown");
            currentRot = slowdownRot;
        }
        // ドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MouseButtonUp");
            currentTime = (slowdownRot * currentTime) / attenuationRot;
            currentRot = attenuationRot;
        }

        // 回転する
        RotatePlayer();
        // 前に進む
        MovePlayer();
	}

    void SetRot( float value )
    {
//        Debug.Log("SetRot:" + Time.time + ", " + value);
        if (-braking < value && value < braking) return;

        // 回転量のブレンド
        rotVec.y = Mathf.Lerp(rotVec.y, rotVec.y + value, blending);
        // 減衰リセット
        rotVecStartY = rotVec.y;
        currentTime = 0.0f;
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
        // んーForceじゃ厳しいか・・・
        Vector3 vec = moveSpeed * transform.forward.normalized;
        //Debug.Log(transform.forward.normalized);
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        //rigidbody.AddForce(transform.forward * velocity * Time.deltaTime, forcemode);
    }

}
