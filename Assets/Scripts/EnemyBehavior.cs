using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    //    [SerializeField]
    //    private string targetTag;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private float interval = 60;
    [SerializeField]
    private float viewRadius = 2.0f;
    [SerializeField]
    private float viewAngle = 30.0f;
    [SerializeField]
    private float movingArea = 10.0f;

    enum State
    {
        Idel,         // �ҋ@
        RandomWalk,   // �����_���E�H�[�N
        Tracking      // �ǐՃ��[�h
    };
    [SerializeField]
    private State defaultState;
    private State state;

    private float counter;

    private GameObject target;
    private CharacterController controller;

    private Vector3 moveVec;
    private Vector3 aimVec;

    // Use this for initialization
    void Start()
    {
        moveVec = new Vector3();
        aimVec  = new Vector3();
        target = GameObject.FindGameObjectWithTag("Player");
        controller = gameObject.GetComponent<CharacterController>();
        state = defaultState;
        counter = 0.0f;
        // �R���[�`���Œ���X�V
        StartCoroutine("BehaviorUpdate");
    }

    bool SearchPlayer()
    {
        if (target == null) return false;
        Vector3 direction = target.transform.position - transform.position;
        Debug.Log("Enemy->Player Direction=" + direction);
        if (direction.magnitude > viewRadius) return false;   // ����ɓ����Ă��Ȃ�

        float angle = Vector3.Angle(transform.forward, direction);
        Debug.Log("Enemy->Player Angle=" + angle);
        if (angle < -viewAngle || angle > viewAngle) return false;  // ����p�ɓ����Ă��Ȃ�

        // �Ԃɏ�Q��������
        //        if( Physics.Raycast( transform.position, direction, direction.magnitude, "");
        //        float sub = Vector3.RotateTowards(vec, transform.forward, 360.0f);

        return true;
    }

    private void Teminate(){
        StopCoroutine("BehaviorUpdate");
        Destroy(gameObject);
    }

    private IEnumerator BehaviorUpdate()
    {
        yield return new WaitForSeconds(interval);
        switch( state ) {
            case State.Idel: BehaviorUpdate_Idel(); break;
            case State.RandomWalk: BehaviorUpdate_RamdomWalk(); break;
            case State.Tracking: BehaviorUpdate_Tracking(); break;
        }
        StartCoroutine("BehaviorUpdate");
    }

    private void BehaviorUpdate_Idel()
    {
        if (SearchPlayer())
        {
            state = State.Tracking;
        }
        else
        {
            Debug.Log("Ideling");
            moveVec.x = 0.0f;
            moveVec.z = 0.0f;
            aimVec.x = 0.0f;
            aimVec.z = 0.0f;
        }
    }

    private void BehaviorUpdate_RamdomWalk()
    {
        if (SearchPlayer())
        {
            state = State.Tracking;
        }
        else
        {
            Debug.Log("RandomWalk");
            // ���̖ڕW�l�ݒ�
            aimVec.Set(transform.position.x, 0.0f, transform.position.z);
            aimVec.x += Random.Range(-1.0f, 1.0f) * movingArea;
            aimVec.z += Random.Range(-1.0f, 1.0f) * movingArea;
        }
    }
    private void BehaviorUpdate_Tracking()
    {
        if (!SearchPlayer())
        {
            state = defaultState;
        }
        else
        {
            if (target == null) return;
            Debug.Log("Tracking");
            //                transform.LookAt(target.gameObject.transform);
            //                transform.forward.normalized;
            //                vec *= speed;
        }
    }

    private void Update_Idel()
    {
    }
    private void Update_RamdomWalk()
    {
		transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);
        moveVec.x = ;
    }
    private void Update_Tracking()
    { 
    }

    void Update()
    {
//        Debug.Log("vec" + moveVec);    // �R���[�`�����������Ă������͕K���ʂ�
        switch (state)
        {
            case State.Idel: Update_Idel(); break;
            case State.RandomWalk: Update_RamdomWalk(); break;
            case State.Tracking: Update_Tracking(); break;
        }
        controller.SimpleMove(moveVec * Time.deltaTime);
    }
}
