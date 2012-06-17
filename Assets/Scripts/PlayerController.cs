using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public class SpeedValue
    {
        public float current = 1.0f;
        [SerializeField]
        private float max = 10.0f;
        [SerializeField]
        private float step = 0.5f;

        public void Add( float value )
        {
            current += value * step;
            if (current < 0.0f) current = 0.0f;
            else if (current > max) current = max;
        }
    };
    [SerializeField]
    private SpeedValue speed;


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

    [System.Serializable]
    public class SwingValue
    {
        public float torque = 0.1f;
        public float force = 0.1f;
    };
    [SerializeField]
    private SwingValue swing;

    private Vector3 rotVec;
    private float rotVecStartY;
    private float currentTime;
    private float currentRot;

    private Quaternion deltaRot;

    private UIController uiCompass;

//    private CharacterController controller;

	void Start () 
    {
//        Screen.lockCursor = true;
//        controller = gameObject.GetComponent<CharacterController>();
        GameObject uiObj = GameObject.Find("/UI");
        if (uiObj) {
            uiCompass = uiObj.GetComponent<UIController>();
        }
        currentRot = attenuationRot;
        if (braking < 0.0f) braking = -braking;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player.OnCollisionEnter:" + collision.collider.name + ":" + Time.time);
        if (collision.collider.CompareTag("Barriar"))
        {
            // 壁に接触
            /*
            Vector3 colvec = -collision.contacts[0].normal;
            float angle = Vector3.Angle(colvec, transform.forward);
            Debug.Log("Angle=" + angle);
            rigidbody.AddTorque(new Vector3(0.0f, (angle >= 0.0f) ? swing.torque : -swing.torque, 0.0f), ForceMode.VelocityChange);
            rigidbody.AddForce(transform.forward * swing.force, ForceMode.VelocityChange);
             */
        }
        else { 
        }
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
            //Debug.Log("rot=" + currentRot + ", time="+ currentTime + ", Rot=" + rotVec.y);
        }

        // ドラッグ中
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("MouseButton :" + Input.GetAxis("Mouse X"));
            // 回転
            SetRot(Input.GetAxis("Mouse X"));

            // 加速
            speed.Add( Input.GetAxis("Mouse Y") );
        }

        // ドラッグ開始
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("MouseButtonDown");
            currentRot = slowdownRot;
        }
        // ドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("MouseButtonUp");
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
        value *= rotSpeed;
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
        Vector3 vec = speed.current * transform.forward.normalized;
        //Debug.Log(transform.forward.normalized);
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        //rigidbody.AddForce( vec * Time.deltaTime, ForceMode.Force);
    }

}
