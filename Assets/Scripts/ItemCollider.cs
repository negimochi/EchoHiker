using UnityEngine;
using System.Collections;

/// <summary>
/// �A�C�e���̐ڐG����
/// </summary>
public class ItemCollider : MonoBehaviour {

   // [SerializeField]
   // private int type;

	// Use this for initialization
	void Start () {
        collider.isTrigger = true;  // �g���K�[�����ĂĂ���
	}

    void OnTriggerEnter( Collider collider )
    {
//        Debug.Log("Collider Enter:" + gameObject.name );
//        GUIDisplay disp = collider.GetComponent<GUIDisplay>();
//        if (disp != null)
        if (collider.gameObject.tag.Equals("Player"))
        {
//            disp.OnHitItem(type);
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject player = collider.gameObject;
            player.SendMessage("OnHitItem", gameObject.name);
            Note note = GetComponent<Note>();
            if (note) note.SendMessage("OnHitItem");
        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit:" + gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
        }
    }
     */

	// Update is called once per frame
	void Update () {
	
	}
}
