﻿using UnityEngine;
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
    public float inflateAirRate = 0.5f;
    [SerializeField]
    public int step = 10;

    [SerializeField]
    private int scoreValue = 100;
    GameObject uiObj = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
        // 獲得ポイントを散らす
        scoreValue = scoreMax;
        StartCoroutine("ChangeScoreValue");
    }

    private IEnumerator ChangeScoreValue()
    {
        yield return new WaitForSeconds(waitTime);

        scoreValue = Mathf.Clamp(scoreValue - step, scoreMin, scoreMax);
        if (scoreValue > scoreMin)
        {
            StartCoroutine("ChangeScoreValue");
        }
        else {
            // 強制削除
            OnDestroyObject();
        }
    }

    void OnDestroyObject()
    {
        // 親から消してもらう
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject, SendMessageOptions.DontRequireReceiver);
        StopAllCoroutines();
        //Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   // プレイヤーか判定
        {
            // スコア加算
            if (uiObj) uiObj.BroadcastMessage("OnGetScore", scoreValue, SendMessageOptions.DontRequireReceiver);
            // ヒット後の自分の処理
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Colliderを切る
            collider.enabled = false;
        }
    }

}
