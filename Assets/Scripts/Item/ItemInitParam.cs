using UnityEngine;
using System.Collections;

public class ItemInitParam : MonoBehaviour
{
    [SerializeField]
    private float maxDeathTime = 60.0f;
    [SerializeField]
    private float minDeathTIme = 10.0f;

    void Start()
    { 
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成されたオブジェクトに対して設定
    }


}
