using UnityEngine;
using System.Collections;

public class CautionUpdater : MonoBehaviour
{
    [SerializeField]
    private float maxWaitTime = 1.0f;

    [SerializeField] // debug
    private int instantiatedCount = 0;

    private GameObject uiObj = null;
    private GameObject maxCautionEnemy = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
    }

    void OnInstantiatedChild(GameObject target)
    {
        instantiatedCount++;
        EnemyCaution enemyCaution = target.GetComponent<EnemyCaution>();
        enemyCaution.SetCountUp(maxWaitTime / (float)instantiatedCount);
        // 通常ゼロになっているはずだが、念のためUpdate
        DisplayValue(target, GetCaution(target));
    }

    void OnDestroyChild(GameObject target)
    {
        if (target.Equals(maxCautionEnemy))
        {
            maxCautionEnemy = null;
            uiObj.BroadcastMessage("OnUpdateCaution", 0, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void DisplayValue(GameObject updateEnemy, int newValue)
    {
        //Debug.Log(updateEnemy.name + ".cation=" + newValue);
        int maxValue = 0;
        if (!updateEnemy.Equals(maxCautionEnemy))
        {
            // 同一でないなら現状のMax値を持つ敵の現在の値と比較
            maxValue = GetCaution(maxCautionEnemy);
            if (newValue > maxValue)
            {
                maxValue = newValue;
                maxCautionEnemy = updateEnemy;
            }
        }
        else
        {
            // 同一ならそのまま更新
            maxValue = newValue;
        }
        // 最大値を表示用に通知
        uiObj.BroadcastMessage("OnUpdateCaution", maxValue, SendMessageOptions.DontRequireReceiver);
    }

    private int GetCaution(GameObject enemyObj)
    {
        if(enemyObj == null ) return 0;
        EnemyCaution enemyCauiton = enemyObj.GetComponent<EnemyCaution>();
        if (enemyCauiton) return enemyCauiton.Value();
        return 0;
    }
}
