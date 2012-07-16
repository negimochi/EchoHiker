using UnityEngine;
using System.Collections;

public class EnemyCaution : MonoBehaviour {

    [SerializeField]
    public float waitTime = 0.5f;
    [SerializeField]
    public int stepMax = 20;
    [SerializeField]
    public int stepMin = 1;

    private int currentStep = 1;

    [SerializeField]    // Debug�{���p
    private int cautionValue = 0;

    private bool countUp = false;
    private bool isCounting = false;
    private CuationUpdater updater = null;

	void Start () 
    {
        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
        if (managerObj) updater = managerObj.GetComponent<CuationUpdater>();
	}

    void OnStayPlayer( float rate )
    {
        if (!countUp) countUp = true;
        if (!isCounting) StartCoroutine("Counter");
        currentStep = (int)Mathf.Lerp(stepMax, stepMin, rate);
    }
    void OnExitPlayer( )
    {
        if (countUp){ countUp = false; }
        if (!isCounting) StartCoroutine("Counter");
    }

    private IEnumerator Counter()
    {
        isCounting = true;
        yield return new WaitForSeconds(waitTime);

        // �X�V
        if (countUp) cautionValue += currentStep;
        else cautionValue -= currentStep;

        // �����`�F�b�N
        if (cautionValue >= 100)
        {
            isCounting = false;
            cautionValue = 100;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            isCounting = false;
            cautionValue = 0;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else StartCoroutine("Counter");
        // �\���X�V
        if (updater) updater.DisplayValue(gameObject, cautionValue);
    }

    public int Value(){ return cautionValue; }
}
