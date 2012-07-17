using UnityEngine;
using System.Collections;

public class EnemyCaution : MonoBehaviour {

    [SerializeField]
    public float waitTime = 0.5f;
    [SerializeField]
    public int countUpStepMax = 20;
    [SerializeField]
    public int countUpStepMin = 1;

    [SerializeField]
    public int countDownStep = 1;


    private int currentStep = 1;

    [SerializeField]    // Debug閲覧用
    private int cautionValue = 0;

    private bool countUp = false;
    private bool counting = false;
    private CuationUpdater updater = null;

	void Start () 
    {
        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
        if (managerObj) updater = managerObj.GetComponent<CuationUpdater>();
	}

    void OnStayPlayer( float rate )
    {
        // 距離が近いほど早く100%になる
        currentStep = (int)Mathf.Lerp(countUpStepMax, countUpStepMin, rate);

        if (!countUp) countUp = true;
        if (!counting)
        {
            // [Caution] 警戒状態 
            counting = true;
            SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
            StartCoroutine("Counter");
        }
    }
    void OnExitPlayer( )
    {
        // 固定
        currentStep = countDownStep;

        if (countUp) countUp = false;
        if (!counting)
        {
            // [Caution] 警戒状態 
            counting = true;
            SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
            StartCoroutine("Counter");
        }
    }

    /// <summary>
    /// Cautionカウンター
    /// </summary>
    /// <returns></returns>
    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(waitTime);

        // 更新
        if (countUp) cautionValue += currentStep;
        else cautionValue -= currentStep;

        // 条件チェック
        if (cautionValue >= 100)
        {
            // [Emergency] Playerを発見 
            counting = false;
            cautionValue = 100;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            // [Emergency] Playerを見失う
            counting = false;
            cautionValue = 0;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // カウンタを繰り返す
            StartCoroutine("Counter");
        }
        // 表示更新
        if (updater) updater.DisplayValue(gameObject, cautionValue);
    }

    public int Value(){ return cautionValue; }
}
