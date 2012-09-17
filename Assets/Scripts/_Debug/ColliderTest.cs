using UnityEngine;
using System.Collections;

public class ColliderTest : MonoBehaviour {

	// Use this for initialization
    BoxCollider boxCollider;

    public float timeUpdate = 1.0f;

    private float counter = 0.0f;

	void Start () {
        boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        counter += Time.deltaTime;
        boxCollider.size = new Vector3( Mathf.Lerp(boxCollider.size.x, 20, Time.deltaTime), boxCollider.size.y, boxCollider.size.z );
	}
}
