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
        [SerializeField]
        private float usualMax = 5.0f;
        [SerializeField]
        private float emergencyMax = 10.0f;

        private float current = 1.0f;
        private float max;
        public float Value
        {
            set
            {
                if (max < value) current = max;
                else current = value;
            }
            get { return current; }
        }

        public void Usual() { max = usualMax; }
        public void Emergency(){   max = emergencyMax; }

        public void Stop() { current = 0.0f; }

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
        [SerializeField]
        private float usualMax = 20.0f;
        [SerializeField]
        private float emergencyMax = 30.0f;
        [SerializeField]
        private float swingStep = 20.0f;
        [SerializeField]
        private float blending = 0.8f;
        [SerializeField]
        private float attenuation = 0.2f;

        private Vector3 current = Vector3.zero;
        private float max;
        private float attenuationStart;
        private float attenuationTime;

        public Vector3 Value
        {
            set
            {
                if (value.y < -max) current.y = -max;
                if (value.y > max) current.y = max;
                else current = value;
            }
            get { return current; }
        }

        public void Usual() { max = usualMax; }
        public void Emergency() { max = emergencyMax; }

        public void Stop() { current = Vector3.zero; }

        public void Swing( float value )
        {
            if (value < -swingStep) value = -swingStep;
            if (value > swingStep) value = swingStep;
            current.y = value;
        }

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
            if (current.y == 0.0f) return false;

            attenuationTime += time;
            current.y = Mathf.SmoothStep(attenuationStart, 0.0f, attenuation * attenuationTime);
            return true;
        }
    };
    [SerializeField]
    private RotationValue rot;

    [SerializeField]
    private Rect runningArea;   // �ړ��͈�
    [SerializeField]
    private float waitTime = 10.0f;

    [SerializeField]
    private float attackWait = 5.0f;

    private float currentTime;
    private bool valid;

//    private bool attack;
    private float currentAttackTime;
    private TorpedoGenerator torpedo;
    private GameObject player;

    /// <summary>
    /// �O������
    /// </summary>
    void OnGameOver()
    {
        speed.Stop();
        rot.Stop();
        valid = false;
    }

    void OnHit()
    {
        // �Փ˂𖳌���
        SphereCollider collider = GetComponent<SphereCollider>();
        if(collider) collider.enabled = false;

        StopAllCoroutines();
    }

    void OnDestroyObject()
    {
        Debug.Log("EnemyBehavior.OnDestroy");
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }

    void Start()
    {
//        attack = false;
        player = GameObject.Find("/Player");
        torpedo = GetComponent<TorpedoGenerator>();
        currentTime = 0.0f;
        valid = true;
        speed.Usual();
        rot.Usual();
    }

    void Update()
    {
        // ��]�̌���
        if (! rot.Attenuate(Time.deltaTime))
        {
            if (valid)
            {
                // �����I����A�J�E���g���čēx�ړ�
                currentTime += Time.deltaTime;
                if (currentTime > waitTime) AutoController();
            }
        }
        // ��]����
        Rotate();
        // �O�ɐi��
        MoveForward();
    }

    public void Emergency()
    {
        rot.Emergency();
        speed.Emergency();
//        attack = true;
        StartCoroutine("AutoAttack");
    }

    public void Caution()
    {
        rot.Emergency();
        speed.Emergency();
//        attack = false;
        StopCoroutine("AutoAttack");
    }

    public void Usual()
    {
        rot.Usual();
        speed.Usual();
//        attack = false;
        StopCoroutine("AutoAttack");
    }

    private IEnumerator AutoAttack()
    {
        yield return new WaitForSeconds(attackWait);

        transform.LookAt(player.transform);

        torpedo.Generate();

        StartCoroutine("AutoAttack");
    }


    /// <summary>
    /// �ړ��̎����X�V
    /// </summary>
    private void AutoController()
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
        if (!runningArea.Contains(new Vector2(transform.position.x, transform.position.z)))
        {
            // �ړ��G���A�O�����������
            Vector3 aimVec = -transform.position.normalized;
            float angle = Vector3.Angle(transform.forward, aimVec);
            Debug.Log("angle=" + angle + ": (" + transform.position.x + "," +  transform.position.z + ")");
            if (!Mathf.Approximately(angle, 0.0f)) rot.Swing(-angle);
        }
        Quaternion deltaRot = Quaternion.Euler(rot.Value * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRot);
    }

    /// <summary>
    /// �ړ��X�V
    /// </summary>
    private void MoveForward()
    {
        Vector3 deltaVec = speed.Value * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + deltaVec * Time.deltaTime);
    }

}
