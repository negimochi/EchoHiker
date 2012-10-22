using UnityEngine;
using System.Collections;

public class SonarMessage : MonoBehaviour {

    [SerializeField]
    private string enemyDestroyed = "The enemy is destroyed!";
    [SerializeField]
    private string itemFound = "You found the item!";
    [SerializeField]
    private string itemLost = "The item was lost...";

    void Start() 
    {
        guiText.text = "";
        guiText.enabled = false;
    }


    void OnEndEnemyDestroyed()
    {
        guiText.text = enemyDestroyed;
        SendMessage("OnStartTextBlink");
    }

    void OnEndItemFound()
    {
        guiText.text = itemFound;
        SendMessage("OnStartTextBlink");
    }

    void OnEndItemLost()
    {
        guiText.text = itemLost;
        SendMessage("OnStartTextBlink");
    }

    void OnEndTextBlink()
    {
        guiText.enabled = false;
    }
}
