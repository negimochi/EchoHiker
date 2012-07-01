using UnityEngine;
using System.Collections;

public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float invalidTime = 2.0f;

    [SerializeField]
    private int damegeValue = 100;
    [SerializeField]
    private int enamyDestroyScore = 100;

    private bool valid = false;
   
    public void SetOwner(OwnerType type)
    {
        owner = type;
    }

	void Start () 
    {
        // 発射の瞬間は、自機にぶつかるのでWaitを挟む
        StartCoroutine("Wait");
	}

    void OnTriggerEnter(Collider other)
    {
        if (valid == true)
        {
            CheckPlayer(other.gameObject);
            CheckEnemy(other.gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (valid == true)
        {
            CheckPlayer(other.gameObject);
            CheckEnemy(other.gameObject);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(invalidTime);
        valid = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CheckPlayer(GameObject target)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (target.tag.Equals("Player"))
        {
            // 自分にヒット

            // ダメージを伝える
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnDamege", damegeValue);

            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);

            valid = false;
        }
    }
    private void CheckEnemy(GameObject target)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (target.tag.Equals("Enemy"))
        {
            if (owner == OwnerType.Player)
            {
                GameObject ui = GameObject.Find("/UI");
                if (ui) ui.SendMessage("OnDestroyEnemy", enamyDestroyScore);
            }
            // 敵にヒット
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);

            valid = false;
        }
    }
}
