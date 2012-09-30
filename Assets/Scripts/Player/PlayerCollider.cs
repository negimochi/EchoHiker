using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    [SerializeField]
    private float speedDown = 2.0f;

    [SerializeField]
    private string damageObjTag = "Torpedo";
    [SerializeField]
    private string terrainTag = "Terrain";

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
        if (valid) CheckDamageCollision(collision.gameObject);
        else CheckTerrainCollision(collision.gameObject);
    }
    void OnCollisionStay(Collision collision)
    {
        if (valid) CheckDamageCollision(collision.gameObject);
    }

    private void CheckDamageCollision(GameObject target)
    {
        // 若干スピードを落とす微調整(あまりスピードがありすぎるとexplosionがきかない)
        if (target.CompareTag(damageObjTag))
        {
            controller.AddSpeed( -speedDown );
        }
    }

    private void CheckTerrainCollision(GameObject target)
    {
        if (target.CompareTag(terrainTag))
        {
            PlayAudioAtOnce();
        }
    }

    private void PlayAudioAtOnce()
    {
        if (!audio) return;
        if (audio.isPlaying) return;
        audio.Play();
    }
}
