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

    void Start()
    {
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
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject, SendMessageOptions.DontRequireReceiver);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   // プレイヤーか判定
        {
            // スコア加算
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.BroadcastMessage("OnGetScore", scoreValue, SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Colliderを切る
            collider.enabled = false;
        }
    }
}
