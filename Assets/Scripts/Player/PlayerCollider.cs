using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    [SerializeField]
    private float speedDown = 2.0f;

    [SerializeField]
    private string tag = "Torpedo";

    private PlayerController controller;
    private bool valid = true;

	// Use this for initialization
	void Start () 
    {
        // コントローラー
        controller = GetComponent<PlayerController>();
	}

    void OnGameOver()
    {
        valid = false;
    }
    void OnGameClear()
    {
        valid = false;
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
        if (!valid) return;
        if (target.CompareTag(tag))
        {
            Debug.Log("Speed Down");
            controller.AddSpeed( -speedDown );
        }
    }
}
