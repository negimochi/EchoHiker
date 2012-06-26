using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

    [SerializeField]
    public int damegeValue = 100;

    private bool cautionArea;

	void Start () 
    {
        cautionArea = false;
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            cautionArea = true;
            Debug.Log("CautionAreaIn:"+cautionArea);
        /*
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnHitDamege", damegeValue);
         */
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            cautionArea = false;
            Debug.Log("CautionAreaOut:" + cautionArea);
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
    }
}
