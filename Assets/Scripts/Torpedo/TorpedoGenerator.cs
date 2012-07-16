using UnityEngine;
using System.Collections;

public class TorpedoGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private float coolTime = 3.0f;  // クールタイム
    [SerializeField]
    private bool sound = false;    // 音を出すか
    [SerializeField]
    private bool sonar = false;  // ソナー表示するか
    [SerializeField]
    private TorpedoCollider.OwnerType type = TorpedoCollider.OwnerType.Enemy;
                                        // 所有者

    private float current;

    private bool valid = true;
    private GameObject parentObj = null;

    void Start()
    {
        // 魚雷の配置先
        parentObj = GameObject.Find("/Object/TorpedoManager");
    }

    void Update()
    {
        if (!valid)
        {
            // クールタイム計測
            current += Time.deltaTime;
            if (current >= coolTime)
            {
                valid = true;
            }
        }
    }

    public void Generate()
    {
        // クールタイム中は生成しない
        if (valid == false)
        {
            Debug.Log("Cool time:" + Time.time);
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

        // Owner設定
        TorpedoCollider torpedoCollider = newObj.GetComponent<TorpedoCollider>();
        if (torpedoCollider) torpedoCollider.SetOwner(type);
        else Debug.LogError("Not exists TorpedoCollider");

        // 音の設定
        Note note = newObj.GetComponentInChildren<Note>();
        if (note) note.SetEnable(sound);
        else Debug.LogError("Not exists Note");

        // ソナーの設定
        ColorFader colorFader = newObj.GetComponentInChildren<ColorFader>();
        if (colorFader) colorFader.SetEnable(sonar);
        else Debug.LogError("Not exists ColorFader");

        // クールタイム開始
        valid = false;
        current = 0.0f;
    }

}
