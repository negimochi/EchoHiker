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

    enum State
    {
        Idel,         // 待機
        RandomWalk,   // ランダムウォーク
        Tracking      // 追跡モード
    };
    [SerializeField]
    private State defaultState;
    private State state;

    private float counter;

    private GameObject target;
    private CharacterController controller;
    private Vector3 moveVec;

    // Use this for initialization
    void Start()
    {
        moveVec = new Vector3();
        moveVec.Normalize();
        target = GameObject.FindGameObjectWithTag("Player");
        controller = gameObject.GetComponent<CharacterController>();
        state = defaultState;
        counter = 0.0f;
    }

    bool SearchPlayer()
    {
        if (target == null) return false;
        Vector3 direction = target.transform.position - transform.position;
        Debug.Log("Enemy->Player Direction=" + direction);
        if (direction.magnitude > viewRadius) return false;   // 視野に入っていない

        float angle = Vector3.Angle(transform.forward, direction);
        Debug.Log("Enemy->Player Angle=" + angle);
        if (angle < -viewAngle || angle > viewAngle) return false;  // 視野角に入っていない
        // 間に障害物がある
        //        if( Physics.Raycast( transform.position, direction, direction.magnitude, "");
        //        float sub = Vector3.RotateTowards(vec, transform.forward, 360.0f);

        return true;
    }

    IEnumerator Ideling(bool isUpdate)
    {
        yield return new WaitForSeconds(interval);
        if (SearchPlayer()) state = State.Tracking;
        else
        {
            moveVec.x = 0.0f;
            moveVec.z = 0.0f;
        }
    }

    void RandomWalk(bool isUpdate)
    {
        if (SearchPlayer()) state = State.Tracking;
        else
        {
            if (isUpdate)
            {
                moveVec.x = Random.Range(-1.0f, 1.0f);
                moveVec.z = Random.Range(-1.0f, 1.0f);
                moveVec *= speed;
            }
        }
    }
    void Tracking(bool isUpdate)
    {
        if (!SearchPlayer()) state = defaultState;
        else
        {
            if (target == null) return;
            if (isUpdate)
            {
                //                transform.LookAt(target.gameObject.transform);
                //                transform.forward.normalized;
                //                vec *= speed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idel:
//                StartCoroutine("Ideling",isUpdate); 
                break;
//            case State.RandomWalk: RandomWalk(isUpdate); break;
//            case State.Tracking: Tracking(isUpdate); break;
        }
        Debug.Log("vec" + moveVec);    // コルーチン発生させてもここは必ず通る
        controller.SimpleMove(moveVec * Time.deltaTime);
    }
}
