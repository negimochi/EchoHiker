using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    [System.Serializable]
    public class SpeedValue
    {
        [SerializeField]
        private float max = 10.0f;
        [SerializeField]
        private float step = 0.5f;

        public float current = 1.0f;

        /// <summary>
        /// �X�s�[�h�ύX
        /// </summary>
        /// <param name="value"></param>
        public void Change( float value )
        {
            current += value * step;
            current = Mathf.Clamp(current, 0.0f, max);
        }

        public void Stop() { current = 0.0f; }

        public float Rate() { return Mathf.InverseLerp(0.0f, max, current); }
    };
    [SerializeField]
    private SpeedValue speed;

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
        /// ��]�ʕύX
        /// </summary>
        public void Change(float value)
        {
            // ���쐫�����コ���邽�߂Ɏ኱�}�[�W����݂���
            if (-margin < value && value < margin) return;

            // ��]�ʂ̃u�����h
            current.y = Mathf.Lerp(current.y, current.y + value, blend);
            if (current.y > max) current.y = max;
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
    private bool valid;

    private Controller controller;
    private MarineSnow marinesnowEffect;
    private TorpedoGenerator torpedo;

	void Start () 
    {
        valid = false;
        // UI�̃R���g���[���[�B�p�ɂɍX�V����̂ŎQ�Ƃ������Ă���
        GameObject uiObj = GameObject.Find("/UI/Controller");
        if (uiObj) 
        {
            controller = uiObj.GetComponent<Controller>();
        }
        // MarinSnow�̃G�t�F�N�g�̓X�s�[�h�ˑ��B�p�ɂɍX�V����̂ŎQ�Ƃ������Ă���
        GameObject effect = GameObject.Find("Effect_MarineSnow");
        if (effect)
        {
            marinesnowEffect = effect.GetComponent<MarineSnow>();
        }
        // �������˃X�N���v�g
        torpedo = GetComponent<TorpedoGenerator>();

        rot.Init();
    }

    void OnGameStart()
    {
        valid = true;
        // �R���g���[���\��
        controller.Enable( true );
    }

    void OnGameOver()
    {
        speed.Stop();
        rot.Stop();

        // �R���g���[���\��
        controller.Enable(false);

        // ���މ��o
        // ���̌Œ���������āA�d�͂�L���ɂ���
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rigidbody.useGravity = true;

        valid = false;
    }
	
	void FixedUpdate () 
    {
        // ��]�̌���
        rot.Attenuate(Time.deltaTime);

        if (valid)
        {
            // ��������
            if (Input.GetKeyDown(KeyCode.B))
            {
                //Debug.Log("B ender : " + Time.time);
                torpedo.Generate();
            }

            // �h���b�O��
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("MouseButton :" + Input.GetAxis("Mouse X"));
                // ��]
                rot.Change(Input.GetAxis("Mouse X"));
                // ����
                speed.Change(Input.GetAxis("Mouse Y"));
            }

            // �h���b�O�J�n
            if (Input.GetMouseButtonDown(0))
            {
                rot.BrakeAttenuation();
            }
            // �h���b�O�I��
            if (Input.GetMouseButtonUp(0))
            {
                rot.UsualAttenuation();
            }
        }
        // ��]����
        Rotate();
        // �O�ɐi��
        MoveForward();
	}
    
    private void Rotate() 
    {
        Quaternion deltaRot = Quaternion.Euler(rot.current * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
        // ��]���o
        controller.SetAngle(transform.localEulerAngles.y);
    }

    private void MoveForward()
    {
        Vector3 vec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        // �X�s�[�h�̕ω����o
        marinesnowEffect.SetSpeed(speed.Rate());
    }

    public void AddSpeed(float value) 
    {
        speed.Change( value );
    }
}
