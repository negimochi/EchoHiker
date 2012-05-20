using UnityEngine;
using System.Collections;

/// <summary>
/// �\�����B
/// </summary>
public class GUIDisplay : MonoBehaviour {

    private string enemyHit         = "Enemey Hits      : ";
    private string itemHitCapsule   = "Item_Capsule Hits: ";
    private string itemHitCube      = "Item_Cube Hits   : ";

    private int itemCapsuleNum = 0;
    private int itemCubeNum = 0;
    private int enemyNum = 0;

    // Use this for initialization
	void Start () {
	
	}

    void OnHitEnemy()
    {
        enemyNum++;
    }
//    public
    void OnHitItem( int itemType )
    {
        if (itemType == 0)
        {
            itemCubeNum++;
        }
        else {
            itemCapsuleNum++;
        }
        /*
        if (itemName == "Item_Cube")
        {
            itemCubeNum++;
        }
        else if (itemName == "Item_Capsule")
        {
            itemCapsuleNum++;
        }
         */
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), enemyHit + enemyNum);
        GUI.Label(new Rect(10, 30, 200, 50), itemHitCube + itemCubeNum);
        GUI.Label(new Rect(10, 50, 200, 70), itemHitCapsule + itemCapsuleNum);
    }
    
}
