using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float attenuationRot = 0.2f;
    [SerializeField]
    private float slowdownRot = 0.5f;
    [SerializeField]
    private float blending = 0.8f;

    private Vector3 rotVec;
    private Quaternion deltaRot;
    private float rotVecStartY;
    private float currentTime;
    private float currentRot;

    private UICompass uiCompass;

    void Start() 
    {
        GameObject uiObj = GameObject.Find("/UI");
        if (uiObj)
        {
            uiCompass = uiObj.GetComponent<UICompass>();
        }
        currentRot = attenuationRot;
    }
    /*
	
	void FixedUpdate () 
    {
        // 回転の減衰
        CalcAttenuation();
        // 回転する
        Rotate();
        // 前に進む
        Move();
    }
     */

    public void CalcAttenuation() 
    {
        if (rotVec.y != 0.0f)
        {
            rotVec.y = Mathf.SmoothStep(rotVecStartY, 0.0f, currentRot * currentTime);
            currentTime += Time.deltaTime;
//            currentTime += 1.0f;
        }
        Debug.Log("rot=" + currentRot + ", time=" + currentTime + ", Rot=" + rotVec.y);
    }

    public void Rotate()
    {
        //rigidbody.AddTorque(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0, forcemode);
        deltaRot = Quaternion.Euler(rotVec * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
        uiCompass.SetAngle(transform.localEulerAngles.y);
    }

    public void Move()
    {
        //velocity = Mathf.Clamp(velocity + Input.GetAxis("Mouse Y") * speed, 0.0f, maxSpeed);
        // んーForceじゃ厳しいか・・・
        Vector3 vec = moveSpeed * transform.forward.normalized;
        //Debug.Log(transform.forward.normalized);
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        //rigidbody.AddForce(transform.forward * velocity * Time.deltaTime, forcemode);
    }

    /// <summary>
    /// 外部からのアクセス要素
    /// </summary>
    public void BeginBraking()
    {
        currentRot = slowdownRot;
    }

    public void FinishBraking()
    {
        currentRot = attenuationRot;
    }

    public void SetRot(float value)
    {
        Debug.Log("SetRot:" + Time.time + "," + value);
        // 回転量のブレンド
        rotVec.y = Mathf.Lerp(rotVec.y, rotVec.y + value, blending);
        // 減衰リセット
        rotVecStartY = rotVec.y;
        currentTime = 0.0f;
    }

}
