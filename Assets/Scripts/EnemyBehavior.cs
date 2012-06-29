using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    /// <summary>
    /// スピード調整
    /// </summary>
    [System.Serializable]
    public class SpeedValue
    {
        public float current = 1.0f;
        public float max;
        [SerializeField]
        private float normalMax = 5.0f;
        [SerializeField]
        private float emergencyMax = 10.0f;

        public void Normal() {      max = normalMax;  }
        public void Emergency(){    max = emergencyMax; }

        /// <summary>
        /// 速度変更
        /// </summary>
        public void Change()
        {
            current += Random.Range(-max, max);
            if (current < 0.0f) current = 0.0f;
            else if (current > max) current = max;
        }
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
        private float max;
        private float attenuationStart;
        private float attenuationTime;

        [SerializeField]
        private float normalMax = 20.0f;
        [SerializeField]
        private float emergencyMax = 30.0f;
        [SerializeField]
        private float blending = 0.8f;
        [SerializeField]
        private float attenuationSpeed = 0.2f;

        public void Normal() { max = normalMax; }
        public void Emergency() { max = emergencyMax; }

        /// <summary>
        /// 回転量変更
        /// </summary>
        public void Change()
        {
            float value = Random.Range(-max, max);
            // 回転量のブレンド
            current.y = Mathf.Lerp(current.y, current.y + value, blending);
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
            if (current.y == 0.0f)
            {
                attenuationTime = 0.0f;
                return false;
            }
            attenuationTime += time;
            current.y = Mathf.SmoothStep(attenuationStart, 0.0f, attenuationSpeed * attenuationTime);
            return true;
        }
    };
    [SerializeField]
    private RotationValue rot;

    [SerializeField]
    private Rect runningArea;   // 移動範囲
    [SerializeField]
    private float waitTime = 10.0f;

    private float currentTime;

    void Start()
    {
        currentTime = 0.0f;

        speed.Normal();
        rot.Normal();
    }

    void Update()
    {
        // 回転の減衰
        if (! rot.Attenuate(Time.deltaTime))
        {
            // 減衰終了後、カウントして再度移動
            currentTime += Time.deltaTime;
            if (currentTime > waitTime) Auto();
        }
        // 回転する
        Rotate();
        // 前に進む
        Move();
    }

    public void Emergency()
    {
        rot.Emergency();
        speed.Emergency();


    }
    public void Normal()
    {
        rot.Normal();
        speed.Normal();
    }

    /// <summary>
    /// 移動の自動更新
    /// </summary>
    private void Auto()
    {
        currentTime = 0.0f;
        rot.Change();
        speed.Change();
    }

    /// <summary>
    /// 回転更新
    /// </summary>
    private void Rotate()
    {
        Quaternion deltaRot = Quaternion.Euler(rot.current * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
    }

    /// <summary>
    /// 移動更新
    /// </summary>
    private void Move()
    {
        Vector3 deltaVec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + deltaVec * Time.deltaTime);
        if (!runningArea.Contains(new Vector2(transform.position.x, transform.position.z)) )
        {
            // 移動エリア外だったら方向を修正
            transform.transform.LookAt(Vector3.zero);
        }
    }

}
