using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour {

//    [SerializeField]
//    private GameObject effect;

	// Use this for initialization
	void Start () {
        //collider.isTrigger = true;  // ÉgÉäÉKÅ[ÇÇΩÇƒÇƒÇ®Ç≠
	}

    void OnTriggerEnter( Collider other )
    {
        Debug.Log("Collider Enter:" + gameObject.name );
        if( other.gameObject.tag.Equals("Player") ){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SendMessage("OnHitItem", gameObject.name );
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
