using UnityEngine;
using System.Collections;

/// <summary>
/// 魚雷の衝突
/// </summary>
public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float delayTime = 2.0f;
    [SerializeField]
    private int damegeValue = 1;

    [System.Serializable]
    public class Explosion
    {
        [SerializeField]
        private float force = 100.0f;
        [SerializeField]
        private float upwardsModifier = 0.0f;
        [SerializeField]
        private ForceMode mode = ForceMode.Impulse;

        private float radius = 3.0f;
            
        public void Add(Rigidbody target, Vector3 pos) 
        {
            target.AddExplosionForce(force, pos, radius, upwardsModifier, mode);
        }
        public void SetRadius(float value) { radius = value; }
    };
    [SerializeField]
    Explosion explosion; 

    private GameObject uiObj = null;

	void Start () 
    {
        uiObj = GameObject.Find("/UI");

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider) explosion.SetRadius( sphereCollider.radius );

        // 発射した自分にヒットするので、発射数秒だけ衝突判定しない。
        collider.enabled = false;
        StartCoroutine("Delay");
	}

    void OnCollisionEnter(Collision collision)
    {
        CollisionCheck( collision.gameObject );
    }
    void OnCollisionStay(Collision collision)
    {
        CollisionCheck( collision.gameObject );
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);

        collider.enabled = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CollisionCheck(GameObject target)
    {
        bool hit = false;
        hit |= CheckPlayer(target);
        hit |= CheckEnemy(target);
        hit |= CheckTorpedo(target);
        if( hit ) {
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider無効化
            collider.enabled = false;
        }
    }

    private bool CheckTorpedo(GameObject target)
    {
        if (target.CompareTag("Torpedo"))
        {
            Debug.Log("CheckTorpedo");
            // 相手の魚雷にヒット
            //target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            return true;
        }
        return false;
    }

    private bool CheckPlayer(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            Debug.Log("CheckPlayer");
            // 衝撃を与える
            explosion.Add( target.rigidbody, transform.position );
 
            // ダメージ通知
            if (uiObj) uiObj.BroadcastMessage("OnDamage", damegeValue, SendMessageOptions.DontRequireReceiver);
            return true;
        }
        return false;
    }

    private bool CheckEnemy(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            Debug.Log("CheckEnemy");
            if (owner == OwnerType.Player)
            {
                // 自分の魚雷が敵にヒットしたときだけ、敵の持っているスコアを加算する通知
                target.SendMessage("OnGetScore", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                // 敵にヒット通知だけ流す
                target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 魚雷を発射したオーナーをセット
    /// </summary>
    /// <param name="type"></param>
    public void SetOwner(OwnerType type) { owner = type; }
    /// <summary>
    /// ダメージ量をセット。通常する必要なし
    /// </summary>
    /// <param name="value"></param>
    public void SetDamageValue(int value) { damegeValue = value; }

}
