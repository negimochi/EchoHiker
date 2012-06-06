using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

    enum MoveType
    {
        None,
        MovePosition,
        
        Math_MoveTowards,
        Math_Repeat,
        Math_Lerp,
        Math_PingPong,
        Math_SmoothStep,
        Math_SmoothDamp,

        AddForce,
        AddRelativeForce,
        AddForceAtPosition,
        AddExplosionForce,
	};
    [SerializeField]
    private MoveType moveType;
	
    public ForceMode forceMode = ForceMode.Force;

    public float magnitude = 1.0f;
    private Vector3 directon;
    private Vector3 firstPos;

    public float smoothTime = 0.3f;
    public float velocity = 0.0f;

    void Start()
    {
        directon = Vector3.right;
        firstPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        

        Move();
    }

    void Move()
    {
        switch (moveType) {
            default: return;

            case MoveType.Math_Repeat:
                transform.position = new Vector3(firstPos.x + Mathf.Repeat(Time.time * magnitude, 20), firstPos.y, firstPos.z);
                break;
            case MoveType.Math_PingPong:
                transform.position = new Vector3(firstPos.x + Mathf.PingPong(Time.time * magnitude, 20), firstPos.y, firstPos.z);
                break;
            case MoveType.Math_SmoothStep:
                transform.position = new Vector3(Mathf.SmoothStep(firstPos.x, 0, Time.time * magnitude), firstPos.y, firstPos.z);
                break;
            case MoveType.Math_Lerp:
                transform.position = new Vector3(Mathf.Lerp(firstPos.x, 0, Time.time * magnitude), firstPos.y, firstPos.z);
                break;
            case MoveType.Math_MoveTowards:
                transform.position = new Vector3(Mathf.MoveTowards(firstPos.x, 0, Time.time * magnitude), firstPos.y, firstPos.z);
                break;
            case MoveType.Math_SmoothDamp:
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.y, 0, ref velocity, smoothTime), firstPos.y, firstPos.z);
                break;
            case MoveType.MovePosition:
                rigidbody.MovePosition(rigidbody.position + directon*magnitude*Time.deltaTime);
                break;
            case MoveType.AddForce:
                rigidbody.AddForce(directon*magnitude*Time.deltaTime, forceMode);
                break;
            case MoveType.AddRelativeForce:
                rigidbody.AddRelativeForce(directon * magnitude * Time.deltaTime, forceMode);
                break;
            case MoveType.AddForceAtPosition:
                if (Input.GetMouseButtonDown(0))
                {
                    rigidbody.AddForceAtPosition(directon * magnitude * Time.deltaTime, new Vector3(-10.0f, 0.0f, 0.0f), forceMode);
                }
                break;
            case MoveType.AddExplosionForce:
                if (Input.GetMouseButtonDown(0))
                {
                    rigidbody.AddExplosionForce(magnitude, transform.position, 1.0f);
                }
                break;
        }
    }
}
