using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    [SerializeField]
    private float speedDown = 2.0f;

    [SerializeField]
    private string damageObjTag = "Torpedo";

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
        // 若干スピードを落とす微調整(あまりスピードがありすぎるとexplosionがきかない)
        if (target.CompareTag(damageObjTag))
        {
            controller.AddSpeed( -speedDown );
        }
    }
}
