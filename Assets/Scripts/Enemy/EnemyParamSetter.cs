using UnityEngine;
using System.Collections;

public class EnemyParamSetter : MonoBehaviour
{
    [SerializeField]
    private EnemyParameter fromParam = new EnemyParameter();
    [SerializeField]
    private EnemyParameter toParam = new EnemyParameter();
    [SerializeField]
    private float duration;

    private float timeStamp = 0.0f;

    void Start()
    {
        timeStamp = Time.timeSinceLevelLoad;
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成されたオブジェクトに対して設定
        EnemyParameter param = new EnemyParameter();
        float t = Time.timeSinceLevelLoad - timeStamp;
        //target.SendMessage("OnStartTimer", param);
    }
}
