using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    /// <summary>
    /// �X�s�[�h����
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
        /// ���x�ύX
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
    /// ��]����
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
        /// ��]�ʕύX
        /// </summary>
        public void Change()
        {
            float value = Random.Range(-max, max);
            // ��]�ʂ̃u�����h
            current.y = Mathf.Lerp(current.y, current.y + value, blending);
            // �������Z�b�g
            attenuationStart = current.y;
            attenuationTime = 0.0f;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="time">���ԕψ�</param>
        /// <returns>������/�������ĂȂ�</returns>
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
    private Rect runningArea;   // �ړ��͈�
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
        // ��]�̌���
        if (! rot.Attenuate(Time.deltaTime))
        {
            // �����I����A�J�E���g���čēx�ړ�
            currentTime += Time.deltaTime;
            if (currentTime > waitTime) Auto();
        }
        // ��]����
        Rotate();
        // �O�ɐi��
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
    /// �ړ��̎����X�V
    /// </summary>
    private void Auto()
    {
        currentTime = 0.0f;
        rot.Change();
        speed.Change();
    }

    /// <summary>
    /// ��]�X�V
    /// </summary>
    private void Rotate()
    {
        Quaternion deltaRot = Quaternion.Euler(rot.current * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
    }

    /// <summary>
    /// �ړ��X�V
    /// </summary>
    private void Move()
    {
        Vector3 deltaVec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + deltaVec * Time.deltaTime);
        if (!runningArea.Contains(new Vector2(transform.position.x, transform.position.z)) )
        {
            // �ړ��G���A�O��������������C��
            transform.transform.LookAt(Vector3.zero);
        }
    }

}
