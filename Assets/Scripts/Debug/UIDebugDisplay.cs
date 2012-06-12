using UnityEngine;
using System.Collections;

public class UIDebugDisplay : MonoBehaviour {

    private Hashtable itemCounter;

	void Start () 
    {
        itemCounter = new Hashtable();
	}

    void OnHitItem(GameObject histObj)
    {
        string objname = histObj.name;
        if (!itemCounter.Contains(objname))
        {
            itemCounter.Add(objname, 1);
        }
        else
        {
            itemCounter[objname] = (int)itemCounter[objname] + 1;
        }
    }

    void OnGUI()
    {
        if (itemCounter.Count == 0) return;
        int count = 0;
        foreach( DictionaryEntry item in itemCounter ) {
            GUI.Label(new Rect(10, 10 + 20 * count, 200, 20 + 20 * count), item.Key + ": " + item.Value);
            count++;
        }
    }
    
}
