using UnityEngine;
using System.Collections;

public class ItemRecovery : MonoBehaviour
{
    [SerializeField]
    private ItemParameter param;

    private float timeStamp = 0.0f; // 経過時間を測るためのタイムスタンプ

    void Start()
    {
    }

    void OnStartLifeTimer(ItemParameter param_)
    {
        Debug.Log("OnStartLifeTimer");
        param = param_;
        //param = new ItemParameter( param_ );
        // タイムスタンプをとっておく。
        timeStamp = Time.timeSinceLevelLoad;
        StartCoroutine("WaitLifeTimeEnd");
    }

    void OnDestroyObject()
    {
        // 念のためコルーチンは切る
        StopAllCoroutines();
    }

    private IEnumerator WaitLifeTimeEnd()
    {
        yield return new WaitForSeconds(param.lifeTime);
        // 寿命まで待ってから消失する
        Disappear();
    }

    private void Disappear()
    {
        Debug.Log("Disappear");
        // 寿命が来たので自分で自分をDestoryする
        GameObject ui = GameObject.Find("/UI");
        // UIにメッセージ通知
        if(ui) ui.BroadcastMessage("OnEndItemLifetime", SendMessageOptions.DontRequireReceiver);
        // 強制削除
        BroadcastMessage("OnHit");
    }

    void OnRecovery()
    {
        // プレイヤーに取得された
        GameObject ui = GameObject.Find("/UI");
        if (ui)
        {
            ui.BroadcastMessage("OnEndItemFound", SendMessageOptions.DontRequireReceiver);

            // UIに通知
            float t = Time.timeSinceLevelLoad - timeStamp;
            
            int score = (int)Mathf.Lerp(param.scoreMax, param.scoreMin, t);
            Debug.Log("Item-Score:" + score);
            ui.BroadcastMessage("OnAddScore",  score );

            int recoveryValue = (int)Mathf.Lerp(param.recoveryMax, param.recoveryMin, t);
            Debug.Log("Item-Air:" + recoveryValue);
            ui.BroadcastMessage("OnAddAir", recoveryValue);
        }
        // ヒット後の自分の処理
        BroadcastMessage("OnHit");
    }

}
