using UnityEngine;
using System.Collections;

public class EnemyParamSetter : MonoBehaviour
{
    [SerializeField]
    private EnemyParameter fromParam = new EnemyParameter();
    [SerializeField]
    private EnemyParameter toParam = new EnemyParameter();
    [SerializeField]
    private float duration = 120.0f;

    private float timeStamp = 0.0f;

    void Start()
    {
        timeStamp = Time.timeSinceLevelLoad;
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成されたオブジェクトに対して設定
        float t = (Time.timeSinceLevelLoad - timeStamp) / duration;
        Debug.Log("EnemyParamSetter" + t);

        EnemyParameter param = new EnemyParameter();
        param.scoreMax = (int)Mathf.Lerp(fromParam.scoreMax, toParam.scoreMax, t);
        param.scoreMin = (int)Mathf.Lerp(fromParam.scoreMin, toParam.scoreMin, t);
        param.cautionUpdateWaitMax = Mathf.Lerp(fromParam.cautionUpdateWaitMax, toParam.cautionUpdateWaitMax, t);
        param.cautionUpdateWaitMin = Mathf.Lerp(fromParam.cautionUpdateWaitMin, toParam.cautionUpdateWaitMin, t);
        param.sonarHitAddCaution = (int)Mathf.Lerp(fromParam.sonarHitAddCaution, toParam.sonarHitAddCaution, t);
        target.SendMessage("OnStartCaution", param);
    }
}
