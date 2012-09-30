using UnityEngine;

[System.Serializable]
public class EnemyParameter 
{

    public int scoreMax = 1000; // スコア最大値
    public int scoreMin = 100;   // スコア最小値

    public float cautionUpdateWaitMax = 1.0f;    // 回復量最大値
    public float cautionUpdateWaitMin = 0.01f;     // 回復量最小値

    public int sonarHitAddCaution = 10;  // 出現時間(この時間を過ぎると自動消失)

    public EnemyParameter(){ }
    public EnemyParameter(EnemyParameter param_)
    {
        scoreMax = param_.scoreMax;
        scoreMin = param_.scoreMin;
        cautionUpdateWaitMax = param_.cautionUpdateWaitMax;
        cautionUpdateWaitMin = param_.cautionUpdateWaitMin;
        sonarHitAddCaution = param_.sonarHitAddCaution;
    }
}
