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
    private int sonarHit = 5;

//    [SerializeField]    // Debug閲覧用
    private int cautionValue = 0;

    public float waitTime   = 1.0f;
    private int currentStep = 1;


    private bool countUp = false;
    private bool counting = false;
    private CuationUpdater updater = null;

	void Start () 
    {
        waitTime = waitTimeMax;
        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
        if (managerObj) updater = managerObj.GetComponent<CuationUpdater>();
	}

    void OnStayPlayer( float distRate )
    {
        // 距離が近いほど早くCautionが上昇する
        waitTime = Mathf.Lerp(waitTimeMin, waitTimeMax, distRate);
        if (!countUp)
        {
            currentStep = step;
            countUp = true;
        }
        StartCount();
    }
    void OnExitPlayer( )
    {
        waitTime = waitTimeMax;
        if (countUp)
        {
            currentStep = -step;
            countUp = false;
        }
        StartCount();
    }

    void OnSonar()
    {
        // ソナーがヒットするたびに、Cautionが上昇
        cautionValue = Mathf.Clamp(cautionValue + sonarHit, 0, 100);
    }

    void StartCount()
    {
        if (counting) return;
        counting = true;
        SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
        StartCoroutine("Counter");
    }

    /// <summary>
    /// Cautionカウンター
    /// </summary>
    /// <returns></returns>
    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue = Mathf.Clamp(cautionValue + currentStep, 0, 100);
        // 表示更新
        updater.DisplayValue(gameObject, cautionValue);
        // 条件チェック
        if (cautionValue >= 100)
        {
            // [Emergency] Playerを発見 
            counting = false;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            // [Emergency] Playerを見失う
            counting = false;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // カウンタを繰り返す
            StartCoroutine("Counter");
        }
    }

    public int Value(){ return cautionValue; }
}
