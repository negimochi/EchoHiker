using UnityEngine;
using System.Collections;

/// <summary>
/// ï\é¶ïîÅB
/// </summary>
public class UIDebugDisplay : MonoBehaviour {

//    private string enemyHit         = "Enemey      : ";
//    private string itemHitCapsule   = "Item_Capsule : ";
//    private string itemHitCube      = "Item_Cube Hits   : ";
//    private string itemHitGold      = "Item_Gold";

    private Hashtable itemCounter;

    // Use this for initialization
	void Start () {
        itemCounter = new Hashtable();
	}

    void OnHitItem(string objName)
    {
        if (!itemCounter.Contains(objName))
        {
            itemCounter.Add(objName, 1);
        }
        else
        {
            itemCounter[objName] = (int)itemCounter[objName] + 1;
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
