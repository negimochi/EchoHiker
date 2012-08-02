using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    [SerializeField]
    private float speedDown = 2.0f;

    private PlayerController controller;

	// Use this for initialization
	void Start () 
    {
        // コントローラー
        controller = GetComponent<PlayerController>();
	}

    void OnCollisionEnter(Collision collision)
    {
        CollisionCheck(collision.gameObject);
    }
    void OnCollisionStay(Collision collision)
    {
        CollisionCheck(collision.gameObject);
    }

    private void CollisionCheck(GameObject target)
    {
        if (target.CompareTag("Torpedo"))
        {
            Debug.Log("Speed Down");
            controller.AddSpeed( -speedDown );
        }
    }
}
