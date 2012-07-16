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

        public void Change( float value )
        {
            current += value * step;
            if (current < 0.0f) current = 0.0f;
            else if (current > max) current = max;
        }

        public void Stop() { current = 0.0f; }
    };
    [SerializeField]
    private SpeedValue speed;

    /// <summary>
    /// 回転調整
    /// </summary>
    [System.Serializable]
    public class RotationValue
    {
        public Vector3 current = Vector3.zero;
        private float attenuationStart;
        private float attenuationTime = 0.0f;
        private float currentRot;

        [SerializeField]
        private float max = 30.0f;
        [SerializeField]
        private float blend = 0.8f;
        [SerializeField]
        private float margin = 0.01f;
        [SerializeField]
        private float attenuationRot = 0.2f;
        [SerializeField]
        private float slowdownRot    = 0.4f;

        public void Init()
        {
            currentRot = attenuationTime;
        }

        /// <summary>
        /// 回転量変更
        /// </summary>
        public void Change(float value)
        {
            if (-margin < value && value < margin) return;

            // 回転量のブレンド
            current.y = Mathf.Lerp(current.y, current.y + value, blend);
            if (current.y > max) current.y = max;
            // 減衰リセット
            attenuationStart = current.y;
            attenuationTime = 0.0f;
        }
        /// <summary>
        /// 減衰
        /// </summary>
        /// <param name="time">時間変位</param>
        /// <returns>減衰中/減衰してない</returns>
        public bool Attenuate(float time)
        {
            if (current.y == 0.0f) return false;

            attenuationTime += time;
            current.y = Mathf.SmoothStep(attenuationStart, 0.0f, currentRot * attenuationTime);
            return true;
        }

        public void BrakeAttenuation() {    currentRot = slowdownRot;   }
        public void UsualAttenuation()
        {
            attenuationTime = (slowdownRot * attenuationTime) / attenuationRot;
            currentRot = attenuationRot;
        }

        public void Stop() { current = Vector3.zero; }
    };
    [SerializeField]
    private RotationValue rot;

    private Quaternion deltaRot;
    private Controller controller;
    private bool valid;

    private TorpedoGenerator torpedo;

	void Start () 
    {
        valid = false;
        GameObject uiObj = GameObject.Find("/UI/Controller");
        if (uiObj) {
            controller = uiObj.GetComponent<Controller>();
        }
        torpedo = GetComponent<TorpedoGenerator>();

        rot.Init();
    }

    void OnGameStart()
    {
        valid = true;
        // コントローラ表示
        controller.Enable( true );
    }

    void OnGameOver()
    {
        speed.Stop();
        rot.Stop();

        // コントローラ表示
        controller.Enable(false);

        // 地面に落とす
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.useGravity = true;

        valid = false;
    }
	
	void FixedUpdate () 
    {
        // 回転の減衰
        rot.Attenuate(Time.deltaTime);

        if (valid)
        {
            // 魚雷発射
            if (Input.GetKeyDown(KeyCode.B))
            {
                //Debug.Log("B ender : " + Time.time);
                torpedo.Generate();
            }

            // ドラッグ中
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("MouseButton :" + Input.GetAxis("Mouse X"));
                // 回転
                rot.Change(Input.GetAxis("Mouse X"));
                // 加速
                speed.Change(Input.GetAxis("Mouse Y"));
            }

            // ドラッグ開始
            if (Input.GetMouseButtonDown(0))
            {
                rot.BrakeAttenuation();
            }
            // ドラッグ終了
            if (Input.GetMouseButtonUp(0))
            {
                rot.UsualAttenuation();
            }
        }
        // 回転する
        Rotate();
        // 前に進む
        MoveForward();
	}
    
    private void Rotate() 
    {
        Quaternion deltaRot = Quaternion.Euler(rot.current * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
        controller.SetAngle(transform.localEulerAngles.y);
    }

    private void MoveForward()
    {
        Vector3 vec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
    }

}
