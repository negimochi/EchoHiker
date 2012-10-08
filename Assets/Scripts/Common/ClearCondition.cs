using UnityEngine;
using System.Collections;

/// <summary>
/// クリア条件
/// </summary>
public class ClearCondition : MonoBehaviour
{
    [SerializeField]
    private bool valid = true;      // 有効ならtrue
    [SerializeField]
    private int destoryNorma = 1;   // 破壊ノルマ

    private GameObject field = null;

    void Start()
    {
        field = GameObject.Find("/Field");
    }

    void OnInstantiatedChild(GameObject target)
    {
        if (!valid) return;

        // 生成したときに条件
    }

    void OnDestroyObject(GameObject target)
    {
        if (!valid) return;

        // 消えたときに条件
        destoryNorma--;
        if (destoryNorma<=0) field.SendMessage("OnClearCondition", target.tag);
    }
}
