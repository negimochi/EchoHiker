using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

    [SerializeField]
    public float waitTime = 0.2f;

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

    private IEnumerator CountupCaution() 
    {
        yield return new WaitForSeconds(waitTime);

        cautionValue++;
        if (cautionValue >= 100)
        {
            EmergencyBehavior(true);
        }
        else
        {
            StartCoroutine("CountupCaution");
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
                behavior.Usual();
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
            StartCoroutine("CountupCaution");
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

}
