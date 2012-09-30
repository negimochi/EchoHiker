using UnityEngine;
using System.Collections;

public class EnemyCaution : MonoBehaviour {

    [SerializeField]
    private EnemyParameter param;

//    [SerializeField]    // Debug閲覧用
    private int cautionValue = 0;

    private int step = 1;
    private int currentStep = 1;
    private float waitTime = 1.0f;

    private bool valid = false;
    private bool counting = false;
    private CautionUpdater updater = null;

	void Start () 
    {
        GameObject managerObj = GameObject.Find("/Field/Enemies");
        if (managerObj) updater = managerObj.GetComponent<CautionUpdater>();
	}

    void OnStayPlayer( float distRate )
    {
        if (!valid) return;
        // 距離が近いほど早くCautionが上昇する
        waitTime = Mathf.Lerp(param.cautionUpdateWaitMin, param.cautionUpdateWaitMax, distRate);

        // カウントしてない場合は開始
        if (counting) return;
        counting = true;
        StartCount(true);
    }

    void OnStartCautionTimer(EnemyParameter param_)
    {
        valid = true;
        param = param_;
        waitTime = param.cautionUpdateWaitMax;
    }


    void OnAddScore()
    {
        if (!valid) return;

        // スコア値を送る
        GameObject ui = GameObject.Find("/UI");
        if (ui)
        {
            ui.BroadcastMessage("OnEndEnemyDestroyed", SendMessageOptions.DontRequireReceiver);
            // 見つかっていないほうが点数が高いように設定
            float time = 1.0f - Mathf.InverseLerp(0, 100, cautionValue);
            int scoreValue = (int)Mathf.Lerp(param.scoreMin, param.scoreMax, time);
            ui.BroadcastMessage("OnAddScore", scoreValue);
        }
        // 自分にヒット判定
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }

    void OnActiveSonar()
    {
        Debug.Log("OnActiveSonar : EnemyCaution");
        // ソナーがヒットするたびに、Cautionが上昇
        cautionValue = Mathf.Clamp(cautionValue + param.sonarHitAddCaution, 0, 100);
    }

    public void SetCountUp( float setWaitTime )
    {
        waitTime = setWaitTime;
        StartCount(true);
    }
    public void SetCountDown( float setWaitTime )
    {
        waitTime = setWaitTime;
        StartCount(false);
    }

    void StartCount(bool isCountup )
    {
        currentStep = (isCountup) ? step : (-step);
        // カウント中はCaution状態
        SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
        // カウンター開始
        StartCoroutine("Counter");
    }

    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue = Mathf.Clamp(cautionValue + currentStep, 0, 100);
        // 表示更新
        updater.DisplayValue(gameObject, cautionValue);
        // 条件チェック
        if (cautionValue >= 100)
        {
            // Playerを発見 
            //counting = false;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            // Playerを見失う
            counting = false;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            StartCoroutine("Counter");
        }
    }

    public int Value(){ return cautionValue; }
}
