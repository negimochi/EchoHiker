using UnityEngine;
using System.Collections;

public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float invalidTime = 1.0f;

    [SerializeField]
    private int damegeValue = 100;
    [SerializeField]
    private int enamyDestroyScore = 100;

    private bool isValid = false;
   
    public void SetOwner(OwnerType type)
    {
        owner = type;
    }

	// Use this for initialization
	void Start () 
    {
        StartCoroutine("Wait");
	}

    void OnTriggerEnter(Collider other)
    {
        if (isValid == true)
        {
            CheckPlayer(other.gameObject);
            CheckEnemy(other.gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (isValid == true)
        {
            CheckPlayer(other.gameObject);
            CheckEnemy(other.gameObject);
        }
    }

	void Update () 
    {
	
	}

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(invalidTime);
        isValid = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CheckPlayer(GameObject target)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (target.tag.Equals("Player"))
        {
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnDamege", damegeValue);

            // ÉqÉbÉgå„ÇÃé©ï™ÇÃèàóù
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);

            isValid = false;
        }
    }
    private void CheckEnemy(GameObject target)
    {
        Debug.Log("Collider Enter:" + gameObject.name);
        if (target.tag.Equals("Enemy"))
        {
            if (owner == OwnerType.Player)
            {
                GameObject ui = GameObject.Find("/UI");
                if (ui) ui.SendMessage("OnDestroyEnemy", enamyDestroyScore);
            }
            // ìGÇ…ÉqÉbÉg
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // ÉqÉbÉgå„ÇÃé©ï™ÇÃèàóù
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);

            isValid = false;
        }
    }
}
