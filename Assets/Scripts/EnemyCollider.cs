using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

    [SerializeField]
    public float cautionUpdateTime = 0.2f;

    private bool isCautionArea;
    private bool isEmargency;

    private int cautionValue = 0;
    public int Caution
    {
        get { return cautionValue;}
    }

	void Start () 
    {
        isCautionArea = false;
        isEmargency = false;
	}

    private IEnumerator CountupCaution(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue++;
        if (cautionValue >= 100)
        {
            EmergencyBehavior(true);
        }
        else
        {
            StartCoroutine("CountupCaution", cautionUpdateTime);
        }
    }

    private void EmergencyBehavior(bool flag)
    {
        isEmargency = flag;
        EnemyBehavior behavior = gameObject.GetComponent<EnemyBehavior>();
        if (behavior) {
            if (isEmargency)
            {
                behavior.Emergency();
            }
            else
            {
                behavior.Normal();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            isCautionArea = true;
            Debug.Log("CautionAreaIn:");
            StartCoroutine("CountupCaution", cautionUpdateTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            isCautionArea = false;
            Debug.Log("CautionAreaOut:");
            StopCoroutine("CountupCaution");

            if (isEmargency) {
                EmergencyBehavior(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
    }
}
