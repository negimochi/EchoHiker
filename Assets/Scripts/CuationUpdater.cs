using UnityEngine;
using System.Collections;

public class CuationUpdater : MonoBehaviour {

    private GameObject uiObj = null;
    private RandomGenerator generator = null;
    private GameObject maxCautionEnemy = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
        generator = GetComponent<RandomGenerator>();
    }

    /*
    private void OnDestroyObject(GameObject obj)
    {
        Debug.Log("EnemyManager.OnDestroyObject");
        list.Remove(obj);
        int result = 0;
        foreach (DictionaryEntry de in list)
        {
            if ((int)de.Value > result) result = (int)de.Value;
        }
        // 表示用に通知
        if(uiObj) uiObj.BroadcastMessage("OnUpdateCaution", result, SendMessageOptions.DontRequireReceiver);
    }
     */
    private void OnUpdateArray( GameObject[] array )
    {
        int maxValue = GetCaution(maxCautionEnemy);
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            int caution = GetCaution(array[i]);
            if (caution > maxValue)
            {
                maxValue = caution;
                maxCautionEnemy = array[i];
            }
        }
        // 最大値を表示用に通知
        uiObj.BroadcastMessage("OnUpdateCaution", maxValue, SendMessageOptions.DontRequireReceiver);
    }

    public void DisplayValue(GameObject updateEnemy, int newValue)
    {
        int maxValue = GetCaution(maxCautionEnemy);
        if (updateEnemy != maxCautionEnemy)
        {
            //int newValue = GetCaution(updateEnemy);
            if (maxValue > newValue)
            {
                maxValue = newValue;
                maxCautionEnemy = updateEnemy;
            }
        }
        // 最大値を表示用に通知
        uiObj.BroadcastMessage("OnUpdateCaution", maxValue, SendMessageOptions.DontRequireReceiver);
    }

    static private int GetCaution(GameObject enemyObj)
    {
        if(enemyObj == null ) return 0;
        EnemyCaution enemyCauiton = enemyObj.GetComponent<EnemyCaution>();
        if (enemyCauiton) return enemyCauiton.Value();
        return 0;
    }
}
