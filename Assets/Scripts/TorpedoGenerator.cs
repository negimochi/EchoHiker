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
    private bool isNote = false;
    [SerializeField]
    private bool isColorFader = false;
    [SerializeField]
    private TorpedoCollider.OwnerType type = TorpedoCollider.OwnerType.Enemy;

    private float current;

    private bool valid = true;
    private GameObject parentObj = null;
   
    public void Generate()
    {
        // 冷却中は生成しない
        if (valid == false)
        {
            Debug.LogWarning("Cooling:" + Time.time);
            return;
        }
        // 位置・角度を求める
        Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        vec += pos.x * transform.right;
        vec += pos.y * transform.up;
        vec += pos.z * transform.forward;
        Quaternion rot = Quaternion.Euler(transform.eulerAngles);
        // 生成
        GameObject newObj = Object.Instantiate(target, vec, rot) as GameObject;
        // 親を設定
        newObj.transform.parent = parentObj.transform;

        {   // Owner設定
            TorpedoCollider torpedoCollider = newObj.GetComponent<TorpedoCollider>();
            if (torpedoCollider) torpedoCollider.SetOwner(type);
            else Debug.LogError("Not exists TorpedoCollider");
        }
        {   // 音の設定
            Note note = newObj.GetComponentInChildren<Note>();
            if (note) note.SetEnable(isNote);
            else Debug.LogError("Not exists Note");
        }
        {   // ソナーの設定
            ColorFader colorFader = newObj.GetComponentInChildren<ColorFader>();
            if (colorFader) colorFader.SetEnable(isColorFader);
            else Debug.LogError("Not exists ColorFader");
        }

        // クールタイム
        valid = false;
        current = 0.0f;
        //StartCoroutine("Cooldown");
    }

    void Update()
    {
        if(!valid) {
            current += Time.deltaTime;
            if (current >= coolTime) {
                valid = true;
            }
        }
    }
    /*
    private IEnumerable Cooldown()
    {
        yield return new WaitForSeconds(coolTime);
        valid = true;
        Debug.Log("Cooldown EndCoroutine");
    }
     */

	// Use this for initialization
	void Start () 
    {
        parentObj = GameObject.Find("/Object/TorpedoManager");
        if (parentObj==null) Debug.LogError("Not exists GameObject(/Object/TorpedoManager)");
	}
	
}
