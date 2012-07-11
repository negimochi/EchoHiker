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
        // ��p���͐������Ȃ�
        if (valid == false)
        {
            Debug.LogWarning("Cooling:" + Time.time);
            return;
        }
        // �ʒu�E�p�x�����߂�
        Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        vec += pos.x * transform.right;
        vec += pos.y * transform.up;
        vec += pos.z * transform.forward;
        Quaternion rot = Quaternion.Euler(transform.eulerAngles);
        // ����
        GameObject newObj = Object.Instantiate(target, vec, rot) as GameObject;
        // �e��ݒ�
        newObj.transform.parent = parentObj.transform;

        {   // Owner�ݒ�
            TorpedoCollider torpedoCollider = newObj.GetComponent<TorpedoCollider>();
            if (torpedoCollider) torpedoCollider.SetOwner(type);
            else Debug.LogError("Not exists TorpedoCollider");
        }
        {   // ���̐ݒ�
            Note note = newObj.GetComponentInChildren<Note>();
            if (note) note.SetEnable(isNote);
            else Debug.LogError("Not exists Note");
        }
        {   // �\�i�[�̐ݒ�
            ColorFader colorFader = newObj.GetComponentInChildren<ColorFader>();
            if (colorFader) colorFader.SetEnable(isColorFader);
            else Debug.LogError("Not exists ColorFader");
        }

        // �N�[���^�C��
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
