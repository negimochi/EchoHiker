using UnityEngine;
using System.Collections;

public class CuationUpdater : MonoBehaviour {

    private GameObject uiObj = null;
    private GameObject maxCautionEnemy = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
    }

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
        // �ő�l��\���p�ɒʒm
        uiObj.BroadcastMessage("OnUpdateCaution", maxValue, SendMessageOptions.DontRequireReceiver);
    }

    public void DisplayValue(GameObject updateEnemy, int newValue)
    {
        //Debug.Log(updateEnemy.name + ".cation=" + newValue);
        int maxValue = 0;
        if (updateEnemy != maxCautionEnemy)
        {
            // ����łȂ��Ȃ猻���Max�l�����G�̌��݂̒l�Ɣ�r
            maxValue = GetCaution(maxCautionEnemy);
            if (newValue > maxValue)
            {
                maxValue = newValue;
                maxCautionEnemy = updateEnemy;
            }
        }
        else
        {
            // ����Ȃ炻�̂܂܍X�V
            maxValue = newValue;
        }
        // �ő�l��\���p�ɒʒm
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
