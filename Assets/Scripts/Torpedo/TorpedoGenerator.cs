using UnityEngine;
using System.Collections;

public class TorpedoGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private float coolTime = 3.0f;  // �N�[���^�C��
    [SerializeField]
    private bool sound = false;    // �����o����
    [SerializeField]
    private bool sonar = false;  // �\�i�[�\�����邩
    [SerializeField]
    private TorpedoCollider.OwnerType type = TorpedoCollider.OwnerType.Enemy;
                                        // ���L��

    private float current;

    private bool valid = true;
    private GameObject parentObj = null;

    void Start()
    {
        // �����̔z�u��
        parentObj = GameObject.Find("/Object/TorpedoManager");
    }

    void Update()
    {
        if (!valid)
        {
            // �N�[���^�C���v��
            current += Time.deltaTime;
            if (current >= coolTime)
            {
                valid = true;
            }
        }
    }

    public void Generate()
    {
        // �N�[���^�C�����͐������Ȃ�
        if (valid == false)
        {
            Debug.Log("Cool time:" + Time.time);
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

        // Owner�ݒ�
        TorpedoCollider torpedoCollider = newObj.GetComponent<TorpedoCollider>();
        if (torpedoCollider) torpedoCollider.SetOwner(type);
        else Debug.LogError("Not exists TorpedoCollider");

        // ���̐ݒ�
        Note note = newObj.GetComponentInChildren<Note>();
        if (note) note.SetEnable(sound);
        else Debug.LogError("Not exists Note");

        // �\�i�[�̐ݒ�
        ColorFader colorFader = newObj.GetComponentInChildren<ColorFader>();
        if (colorFader) colorFader.SetEnable(sonar);
        else Debug.LogError("Not exists ColorFader");

        // �N�[���^�C���J�n
        valid = false;
        current = 0.0f;
    }

}
