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
    [SerializeField]
    private Vector3 endPos = new Vector3(20.0f, 0.0f, 0.0f);

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
        switch (moveType)
        {
            default: return;

            case MoveType.Math_Repeat:   Move_repeat();  break;
            case MoveType.Math_PingPong: Move_pingpong(); break;
            case MoveType.Math_SmoothStep: Move_smoothstep(); break;
            case MoveType.Math_Lerp: Move_Lerp();   break;
            case MoveType.Math_MoveTowards: Move_towrad(); break;
            case MoveType.Math_SmoothDamp: Move_SmoothDamp();   break;
            case MoveType.MovePosition:   Move_MovePosition();  break;
            case MoveType.AddForce:
                rigidbody.AddForce(directon * magnitude * Time.deltaTime, forceMode);
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

    void Move_repeat()
    {
        transform.position = new Vector3(firstPos.x + Mathf.Repeat(Time.time * magnitude, 20), firstPos.y, firstPos.z);
    }
    void Move_pingpong()
    {
        float t = Time.time * magnitude;
//        Debug.Log(t);
        transform.position = new Vector3(firstPos.x + Mathf.PingPong(t, 20), firstPos.y, firstPos.z);
    }
    void Move_smoothstep()
    {
        transform.position = new Vector3(Mathf.SmoothStep(firstPos.x, endPos.x, Time.time * magnitude), firstPos.y, firstPos.z);
    }
    void Move_Lerp()
    {
        transform.position = new Vector3(Mathf.Lerp(firstPos.x, endPos.x, Time.time * magnitude), firstPos.y, firstPos.z);
    }
    void Move_towrad()
    {
        transform.position = new Vector3(Mathf.MoveTowards(firstPos.x, 0, Time.time * magnitude), firstPos.y, firstPos.z);
    }
    void Move_SmoothDamp()
    { 
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.y, 0, ref velocity, smoothTime), firstPos.y, firstPos.z);
    }
    void Move_MovePosition()
    {
//        rigidbody.MovePosition(rigidbody.position + directon * magnitude * Time.deltaTime);
          rigidbody.MovePosition(transform.position + directon * magnitude * Time.deltaTime);
    }
	
	void OnTriggerEnter(Collider other)
	{
//        rigidbody.AddExplosionForce(100.0f, transform.position, 10.0f);
        Debug.Log("OnTriggerEnter:" + other.gameObject.name);
	}
    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay:" + other.gameObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit:" + other.gameObject.name);
    }
    void OnCollisionEnter(Collision collision) 
    {
        ContactPoint contact = collision.contacts[0];
        rigidbody.AddExplosionForce(100.0f, contact.point, 10.0f);
        Debug.Log("OnCollisionEnter:" + collision.collider.gameObject.name);
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay:" + collision.collider.gameObject.name);
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit:" + collision.collider.gameObject.name);
    }
}
