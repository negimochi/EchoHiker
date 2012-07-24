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

//    [SerializeField]    // Debug�{���p
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
        // �������߂��قǑ���Caution���㏸����
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
        // �\�i�[���q�b�g���邽�тɁACaution���㏸
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
    /// Caution�J�E���^�[
    /// </summary>
    /// <returns></returns>
    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue = Mathf.Clamp(cautionValue + currentStep, 0, 100);
        // �\���X�V
        updater.DisplayValue(gameObject, cautionValue);
        // �����`�F�b�N
        if (cautionValue >= 100)
        {
            // [Emergency] Player�𔭌� 
            counting = false;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            // [Emergency] Player��������
            counting = false;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // �J�E���^���J��Ԃ�
            StartCoroutine("Counter");
        }
    }

    public int Value(){ return cautionValue; }
}
