using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    private Hashtable list;
    private GameObject ui = null;

    void Start()
    {
        ui = GameObject.Find("/UI");
        if (ui == null) Debug.LogError( "Not Exists UI GameObject" );
        list = new Hashtable();
    }

    private void OnDestroyObject(string name)
    {
        Debug.Log("EnemyManager.OnDestroyObject");
        list.Remove(name);
    }
    private void OnGenerated(DictionaryEntry target)
    {
        Debug.Log("EnemyManager.OnGenerated");
        list.Add(target.Key,target.Value);
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
        // ’Ê’m
        ui.SendMessage("OnUpdateCaution", result);
    }
}
