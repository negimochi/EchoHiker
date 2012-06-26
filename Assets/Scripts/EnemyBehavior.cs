using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
//    private float ;

    private int cautionRate = 0;
    public int CautionRate
    {
        get { return cautionRate; }
    }

    /*
    enum State
    {
        Idel,         // 待機
        RandomWalk,   // ランダムウォーク
        Tracking      // 追跡モード
    };
    [SerializeField]
    private State state;
    */

    private float counter;

    private GameObject target;

    private Quaternion aimAngle;
    private Vector3 moveVec;
    private Vector3 aimVec;

    void Start()
    {
        moveVec = new Vector3();
        aimVec  = new Vector3();
        target = GameObject.FindGameObjectWithTag("Player");
//        controller = gameObject.GetComponent<CharacterController>();
        counter = 0.0f;
    }

    void Update()
    {
        /*
        switch (state)
        {
            case State.Idel: Update_Idel(); break;
            case State.RandomWalk: Update_RamdomWalk(); break;
            case State.Tracking: Update_Tracking(); break;
        }
        */
        Vector3 vec = speed * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
        //        controller.SimpleMove(moveVec * Time.deltaTime);

    }

/*
    bool SearchPlayer()
    {
        if (target == null) return false;
        Vector3 direction = target.transform.position - transform.position;
        Debug.Log("Enemy->Player Direction=" + direction);
        if (direction.magnitude > viewRadius) return false;   // 視野に入っていない

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
            state = State.RandomWalk;
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
        //transform.rotation = Quaternion.Lerp(transform.rotation, aimAngle, Time.deltaTime * rotateSpeed);
    }
    private void Update_Tracking()
    { 
    }
*/
}
