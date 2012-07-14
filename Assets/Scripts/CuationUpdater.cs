using UnityEngine;
using System.Collections;

public class CuationUpdater : MonoBehaviour {

    private Hashtable list;
    private GameObject ui = null;

    void Start()
    {
        ui = GameObject.Find("/UI");
        list = new Hashtable();
    }

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
        if(ui) ui.BroadcastMessage("OnUpdateCaution", result, SendMessageOptions.DontRequireReceiver);
    }
    private void OnGenerated( GameObject target )
    {
        // 生成したオブジェクトを登録
        Debug.Log("EnemyManager.OnGenerated");
        list.Add(target, 0);
    }

    public void UpdateCautionValue(DictionaryEntry target)
    {
//        Debug.Log("EnemyManager.UpdateCautionValue");
        list[target.Key] = target.Value;
        int result = (int)target.Value;

        foreach( DictionaryEntry de in list ) {
            if( de.Key == target.Key ) continue;
            if( (int)de.Value > result ) result = (int)de.Value;
        }
        // 表示用に通知
        ui.BroadcastMessage("OnUpdateCaution", result, SendMessageOptions.DontRequireReceiver);
    }
}
