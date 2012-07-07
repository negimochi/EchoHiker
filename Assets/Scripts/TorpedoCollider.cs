using UnityEngine;
using System.Collections;

public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float waitTime = 2.0f;
    [SerializeField]
    private int damegeValue = 1;
    [SerializeField]
    private int enamyDestroyScore = 100;

    private GameObject ui = null;

	void Start () 
    {
        ui = GameObject.Find("/UI");
        // 発射の瞬間は、自機にぶつかるのでWaitを挟む
        collider.enabled = false;
        StartCoroutine("Wait");
	}

    void OnTriggerEnter(Collider other)
    {
        CheckPlayer(other.gameObject);
        CheckEnemy(other.gameObject);
        CheckTorpedo(other.gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        CheckPlayer(other.gameObject);
        CheckEnemy(other.gameObject);
        CheckTorpedo(other.gameObject);
    }


    public void SetOwner(OwnerType type)
    {
        owner = type;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        collider.enabled = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CheckTorpedo(GameObject target)
    {
        if (target.CompareTag("Torpedo"))
        { 
            // 相手の魚雷にヒット
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider無効化
            collider.enabled = false;
        }
    }

    private void CheckPlayer(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z + sphereCollider.radius);
            target.rigidbody.AddExplosionForce(100.0f, vec, 10.0f, 3.0f, ForceMode.Impulse);

            // ダメージ通知
            ui.SendMessage("OnDamage", damegeValue, SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider無効化
            collider.enabled = false;
        }
    }
    private void CheckEnemy(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            if (owner == OwnerType.Player)
            {
                // 自分の魚雷が敵にヒット。ポイント通知
                ui.SendMessage("OnDestroyEnemy", enamyDestroyScore);
            }
            // 敵にヒット通知
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider無効化
            collider.enabled = false;
        }
    }
}
