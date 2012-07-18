using UnityEngine;
using System.Collections;

public class EnemyEyeCollider : MonoBehaviour {

    /*
    [SerializeField]
    public float waitTime = 0.2f;
    [SerializeField]
    public int stepMax = 10;
    [SerializeField]
    public int stepMin = 1;
     */

//    private int cautionValue = 0;
//    private int step;
    private float radius;

//    private bool counting = false;
//    private bool isEmargency = false;
//    private CuationUpdater updater = null;
//    private EnemyBehavior behavior = null;
    private GameObject parentObj = null;

	void Start () 
    {
        parentObj = gameObject.transform.parent.gameObject;

//        GameObject managerObj = GameObject.Find("/Object/EnemyManager");
//        if (managerObj) updater = managerObj.GetComponent<CuationUpdater>();

        //if (parentObj) behavior = parentObj.GetComponent<EnemyBehavior>();

//        step = stepMin;
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider) radius = sphereCollider.radius;
	}

    /*
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        if (!isCountUp)
        {
            // 敵のエリアに入った場合
            Debug.Log("OnTriggerEnter:" + transform.parent.gameObject.name);
            SwitchCount(other.gameObject, true);
        }
    }
　  */


    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("TrigStay:" + Time.time);

        float dist = Vector3.Distance(transform.position, other.gameObject.transform.position);
        float t = Mathf.InverseLerp(0.0f, radius, dist);
        parentObj.SendMessage("OnStayPlayer", t, SendMessageOptions.DontRequireReceiver);
    }
    void OnTriggerStay(Collider other)
    {
        // EnterよりStayでとったほうが確実
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("TrigStay:" + Time.time);

        float dist = Vector3.Distance(transform.position, other.gameObject.transform.position);
        float t = Mathf.InverseLerp( 0.0f, radius, dist);
        parentObj.SendMessage("OnStayPlayer", t, SendMessageOptions.DontRequireReceiver);
        /*
        if (!counting)
        {
            Debug.Log("Counting Start:" + parentObj.name);
            SwitchCount(other.gameObject, true);
        }
        else { 
            // 距離に応じて変更
            UpdateStep(other.gameObject);
        }
         */
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("TrigExit:" + Time.time);

        parentObj.SendMessage("OnExitPlayer", SendMessageOptions.DontRequireReceiver);
        /*
        if (!other.gameObject.tag.Equals("Player")) return;
        if (isCountUp)
        {
            // 敵のエリアから外に出た場合
            Debug.Log("OnTriggerExit:" + transform.parent.gameObject.name);
            SwitchCount(other.gameObject, false);

        }
         */
    }
    /*
    private void UpdateStep(GameObject target)
    {
        float dist = Vector3.Distance( transform.position, target.transform.position);
        float t = Mathf.InverseLerp( 0.0f, radius, dist);
        step = (int)Mathf.Lerp(stepMax, stepMin, t);
    }
     */

    /*
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
            parentObj.SendMessage("OnCaution",SendMessageOptions.DontRequireReceiver);
            isCountUp = false;
        }
    }

    private IEnumerator CountUpCaution() 
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue += step;
        if (cautionValue >= 100)
        {
            cautionValue = 100;
            EmergencyBehavior(true);
        }
        else
        {
            StartCoroutine("CountUpCaution");
        }
        if (updater) updater.DisplayValue(parentObj);
    }

    private IEnumerator CountDownCaution()
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue -= 1;
        if (cautionValue <= 0)
        {
            cautionValue = 0;
            EmergencyBehavior(false);
        }
        else
        {
            StartCoroutine("CountDownCaution");
        }
        if (updater) updater.DisplayValue(parentObj);
    }

    private void EmergencyBehavior(bool flag)
    {
        isEmargency = flag;
        if (isEmargency)
        {
            parentObj.SendMessage("OnEmergency", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            parentObj.SendMessage("OnUsual", SendMessageOptions.DontRequireReceiver);
        }
    }
    //public int CautionValue() { return cautionValue; }
    */
}
