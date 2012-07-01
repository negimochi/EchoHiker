using UnityEngine;
using System.Collections;

public class TorpedoGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private float coolTime = 3.0f;
    [SerializeField]
    private TorpedoCollider.OwnerType type = TorpedoCollider.OwnerType.Enemy;

    private bool isCooling;
    private GameObject parentObj;

    public void Generate()
    {
        // 冷却中は生成しない
        if (isCooling == true) return;

        Debug.Log(transform.localPosition + "," + transform.position);
        //Vector3 vec = new Vector3( transform.position.x, transform.position.y,transform.position.z );
        Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        vec += pos.x * transform.right;
        vec += pos.y * transform.up;
        vec += pos.z * transform.forward;
        Quaternion rot = Quaternion.Euler(transform.eulerAngles);
        GameObject newObj = Object.Instantiate(target, vec, rot) as GameObject;
        newObj.transform.parent = parentObj.transform;
        TorpedoCollider torpedoCollider = newObj.GetComponent<TorpedoCollider>();
        torpedoCollider.SetOwner(type);
        // クールタイム
        //isCooling = true;
        //StartCoroutine("CoolDown");
    }

    private IEnumerable CoolDown()
    {
        Debug.Log("CoolDown");
        yield return new WaitForSeconds(coolTime);

        Debug.Log("CoolDown End");
        isCooling = false;
    }

	// Use this for initialization
	void Start () 
    {
        isCooling = false;
        parentObj = GameObject.Find("/Object/TorpedoManager");
	}
	
}
