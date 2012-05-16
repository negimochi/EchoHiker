using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SendMessage("OnHitEnemy");
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
        }
    }

}
