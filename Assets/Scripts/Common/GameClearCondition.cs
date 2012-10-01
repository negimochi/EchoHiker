using UnityEngine;
using System.Collections;

public class GameClearCondition : MonoBehaviour
{
    [SerializeField]
    private bool valid = true;
    [SerializeField]
    private int destoryNorma = 1;

    private GameObject field = null;


    void Start()
    {
        field = GameObject.Find("/Field");
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成したときに条件があればここ
    }

    void OnDestroyObject(GameObject target)
    {
        if (!valid) return;
        // 消えたときに条件
        destoryNorma--;
        if (destoryNorma <= 0)
        {
            if(field) field.SendMessage("OnClearCondition", target.tag);
        }
    }
}
