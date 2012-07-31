using UnityEngine;
using System.Collections;

public class CautionUpdater : MonoBehaviour
{

    private GameObject uiObj = null;
    private GameObject maxCautionEnemy = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
    }

    /*
    private void OnUpdateArray( ArrayList array )
    {
        int maxValue = GetCaution(maxCautionEnemy);
        int size = array.Count;
        for (int i = 0; i < size; i++)
        {
            GameObject target = (GameObject)array[i];
            int caution = GetCaution(target);
            if (caution > maxValue)
            {
                maxValue = caution;
                maxCautionEnemy = target;
            }
        }
        // �ő�l��\���p�ɒʒm
        uiObj.BroadcastMessage("OnUpdateCaution", maxValue, SendMessageOptions.DontRequireReceiver);
    }
     */

    void OnInstantiatedChild(GameObject target)
    {
        // �ʏ�[���ɂȂ��Ă���͂������A�O�̂���Update
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
