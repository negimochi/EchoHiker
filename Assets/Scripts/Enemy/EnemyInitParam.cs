using UnityEngine;
using System.Collections;

public class EnemyInitParam : MonoBehaviour
{
    [SerializeField]
    private float maxCautionWaitTime = 2.0f;
    [SerializeField]
    private float minCautionWaitTIme = 0.1f;

    void Start()
    { 
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成されたオブジェクトに対して設定
    }


}
