using UnityEngine;
using System.Collections;

public class EnemyCaution : MonoBehaviour {

    [SerializeField]
    public float waitTimeMax = 0.40f;
    [SerializeField]
    public float waitTimeMin = 0.01f;
    [SerializeField]
    private int step = 1;
    [SerializeField]
    private int sonarHit = 10;

//    [SerializeField]    // Debug閲覧用
    private int cautionValue = 0;

    public float waitTime   = 1.0f;
    private int currentStep = 1;


    private bool counting = false;
    private CautionUpdater updater = null;

	void Start () 
    {
        waitTime = waitTimeMax;
        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
        if (managerObj) updater = managerObj.GetComponent<CautionUpdater>();
	}

    void OnStayPlayer( float distRate )
    {
        // 距離が近いほど早くCautionが上昇する
        waitTime = Mathf.Lerp(waitTimeMin, waitTimeMax, distRate);

        // カウントしてない場合は開始
        if (counting) return;
        counting = true;
        StartCount(true);
    }
    void OnExitPlayer( )
    {
        // Cautionの値が減少する
        waitTime = waitTimeMin;
        currentStep = -step;
        StartCount(false);
    }

    void OnSonar()
    {
        Debug.Log("HitSonar");
        // ソナーがヒットするたびに、Cautionが上昇
        cautionValue = Mathf.Clamp(cautionValue + sonarHit, 0, 100);
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
