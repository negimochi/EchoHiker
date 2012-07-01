using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour
{
    [SerializeField]
    public float waitTime = 5.0f;
    [SerializeField]
    private int scoreMax = 1000;
    [SerializeField]
    public int scoreMin = 50;
    [SerializeField]
    public int step = 10;

    private int scoreValue = 100;
    private bool isFinished;

    void Start()
    {
        isFinished = false;
        collider.isTrigger = true;  // トリガーをたてておく

        // 乱数で獲得ポイントを散らす
        scoreValue = scoreMax;
        StartCoroutine("ChangeScoreValue");
    }

    private IEnumerator ChangeScoreValue()
    {
        yield return new WaitForSeconds(waitTime);

        scoreValue -= step;
        if(scoreValue < scoreMin) {
           scoreValue = scoreMin;
        }
        else {
            StartCoroutine("ChangeScoreValue");
        }
    }

    void OnDestroyObject()
    {
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject.name, SendMessageOptions.DontRequireReceiver);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isFinished) return; // 1回だけ衝突をみたいのでその監視用。
                                // isTrigge=falseしても複数回とってしまう。

        if (collider.CompareTag("Player"))   // プレイヤーか判定
        {
            isFinished = true;
            // HitItem通知
            //obj.SendMessage("OnHitItem");
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnGetItem", scoreValue, SendMessageOptions.DontRequireReceiver);

            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
