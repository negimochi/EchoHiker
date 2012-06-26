using UnityEngine;
using System.Collections;

public class TorpedoCollider : MonoBehaviour {

    [SerializeField]
    public int damegeValue = 100;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnHitDamege", damegeValue);
        }
    }

    // Update is called once per frame
	void Update () {
	
	}
}
