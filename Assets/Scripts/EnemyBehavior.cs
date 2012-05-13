using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int interval;

    private int counter;
    private GameObject target;

	// Use this for initialization
    void Start()
    {
        counter = 0;
        target = GameObject.FindGameObjectWithTag( targetTag );
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (target == null) return;

        if (counter++ >= interval)
        {
            transform.LookAt( target.gameObject.transform );
           // Debug.Log(target.transform);
            counter = 0;
        }

        Vector3 vec = transform.forward.normalized;
        transform.position += vec * speed;
	}
}
