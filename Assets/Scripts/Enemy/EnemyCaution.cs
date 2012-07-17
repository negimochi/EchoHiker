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

    [SerializeField]    // Debug�{���p
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
        // �������߂��قǑ���100%�ɂȂ�
        currentStep = (int)Mathf.Lerp(countUpStepMax, countUpStepMin, rate);

        if (!countUp) countUp = true;
        if (!counting)
        {
            // [Caution] �x����� 
            counting = true;
            SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
            StartCoroutine("Counter");
        }
    }
    void OnExitPlayer( )
    {
        // �Œ�
        currentStep = countDownStep;

        if (countUp) countUp = false;
        if (!counting)
        {
            // [Caution] �x����� 
            counting = true;
            SendMessage("OnCaution", SendMessageOptions.DontRequireReceiver);
            StartCoroutine("Counter");
        }
    }

    /// <summary>
    /// Caution�J�E���^�[
    /// </summary>
    /// <returns></returns>
    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(waitTime);

        // �X�V
        if (countUp) cautionValue += currentStep;
        else cautionValue -= currentStep;

        // �����`�F�b�N
        if (cautionValue >= 100)
        {
            // [Emergency] Player�𔭌� 
            counting = false;
            cautionValue = 100;
            SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else if (cautionValue <= 0)
        {
            // [Emergency] Player��������
            counting = false;
            cautionValue = 0;
            SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // �J�E���^���J��Ԃ�
            StartCoroutine("Counter");
        }
        // �\���X�V
        if (updater) updater.DisplayValue(gameObject, cautionValue);
    }

    public int Value(){ return cautionValue; }
}
