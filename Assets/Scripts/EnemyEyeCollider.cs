using UnityEngine;
using System.Collections;

public class EnemyEyeCollider : MonoBehaviour {

    [SerializeField]
    public float waitTime = 0.2f;
    [SerializeField]
    public int stepMax = 10;
    [SerializeField]
    public int stepMin = 1;

    private int cautionValue = 0;
    private int step;
    private float radius;

    private bool isCountUp = false;
    private bool isEmargency = false;
    private EnemyManager manager = null;
    private EnemyBehavior behavior = null;

	void Start () 
    {
        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
        if (managerObj) manager = managerObj.GetComponent<EnemyManager>();
        if (manager==null) Debug.LogError("Not Exists EnemyManager");

        GameObject parentObj = gameObject.transform.parent.gameObject;
        if (parentObj) behavior = parentObj.GetComponent<EnemyBehavior>();
        if (behavior == null) Debug.LogError("Not Exists EnemyBehavior");

        step = stepMin;
        radius = GetComponent<SphereCollider>().radius;
	}

    void OnTriggerEnter(Collider other)
    {
        /*
        if (!other.gameObject.tag.Equals("Player")) return;
        if (!isCountUp)
        {
            // 敵のエリアに入った場合
            Debug.Log("OnTriggerEnter:" + transform.parent.gameObject.name);
            SwitchCount(other.gameObject, true);
        }
         */
    }
    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        if (!isCountUp)
        {
            // 出現位置ですでにプレイヤーが射程に入っていた場合
            Debug.Log("OnTriggerStay:" + transform.parent.gameObject.name);
            SwitchCount(other.gameObject, true);
        }
        else { 
            // 距離に応じて変更
            UpdateStep(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        if (isCountUp)
        {
            // 敵のエリアから外に出た場合
            Debug.Log("OnTriggerExit:" + transform.parent.gameObject.name);
            SwitchCount(other.gameObject, false);

        }
    }

    private void UpdateStep(GameObject target)
    {
        float dist = Vector3.Distance( transform.position, target.transform.position);
        float t = Mathf.InverseLerp( 0.0f, radius, dist);
        step = (int)Mathf.Lerp(stepMax, stepMin, t);
    }

    private void SwitchCount(GameObject target, bool flag)
    {
        if (flag)
        {
            Debug.Log("Player: In CautionArea");
            StartCoroutine("CountUpCaution");
            isCountUp = true;
        }
        else
        {
            Debug.Log("Player: Out of CautionArea");
            StartCoroutine("CountDownCaution");
            behavior.Caution();
            isCountUp = false;
        }
    }

    private IEnumerator CountUpCaution() 
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue += step;
        if (manager) manager.UpdateCautionValue( new DictionaryEntry(transform.parent.gameObject, cautionValue) );
        if (cautionValue >= 100)
        {
            cautionValue = 100;
            EmergencyBehavior(true);
        }
        else
        {
            StartCoroutine("CountUpCaution");
        }
    }

    private IEnumerator CountDownCaution()
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue -= 1;
        if (manager) manager.UpdateCautionValue( new DictionaryEntry(transform.parent.gameObject, cautionValue));
        if (cautionValue <= 0)
        {
            cautionValue = 0;
            EmergencyBehavior(false);
        }
        else
        {
            StartCoroutine("CountDownCaution");
        }
    }

    private void EmergencyBehavior(bool flag)
    {
        if (behavior == null) return;
        isEmargency = flag;
        if (isEmargency)
        {
            behavior.Emergency();
        }
        else
        {
            behavior.Usual();
        }
    }

}
