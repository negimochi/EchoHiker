using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    //    [SerializeField]
    //    private string targetTag;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;
    [SerializeField]
    private float interval = 120.0f;
    [SerializeField]
    private float viewRadius = 2.0f;
    [SerializeField]
    private float viewAngle = 30.0f;
    [SerializeField]
    private float movingArea = 10.0f;

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

    private Quaternion aimAngle;
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
        // コルーチンで定期更新
        StartCoroutine("BehaviorUpdate");
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
            // 次の目標値設定
            aimVec.Set(transform.position.x, 0.0f, transform.position.z);
            aimVec.x += Random.Range(-1.0f, 1.0f) * movingArea;
            aimVec.z += Random.Range(-1.0f, 1.0f) * movingArea;
//            aimAngle = Vector3.Angle(transform.forward, aimVec);
            aimAngle = Quaternion.FromToRotation(transform.forward, aimVec);
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
        moveVec = Vector3.Lerp( transform.position, aimVec, Time.deltaTime * speed);
        //Mathf.LerpAngle(aimAngle, Time.deltaTime * rotateSpeed / interval);
        transform.rotation = Quaternion.Lerp(transform.rotation, aimAngle, Time.deltaTime * rotateSpeed);
    }
    private void Update_Tracking()
    { 
    }

    void Update()
    {
        switch (state)
        {
            case State.Idel: Update_Idel(); break;
            case State.RandomWalk: Update_RamdomWalk(); break;
            case State.Tracking: Update_Tracking(); break;
        }
        controller.SimpleMove(moveVec * Time.deltaTime);
    }
}
