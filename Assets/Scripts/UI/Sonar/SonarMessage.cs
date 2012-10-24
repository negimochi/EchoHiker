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

    /// <summary>
    /// �I�u�W�F�N�g�̃q�b�g
    /// </summary>
    /// <param name="tag"></param>
    void OnHitObject( string tag )
    {
        if (tag.Equals("Enemy")) guiText.text = enemyDestroyed;
        else if (tag.Equals("Item")) guiText.text = itemFound;
        // �_�ŊJ�n
        SendMessage("OnStartTextBlink");
    }

    /// <summary>
    /// �I�u�W�F�N�g�̃��X�g
    /// </summary>
    /// <param name="tag"></param>
    void OnLostObject( string tag )
    {
        if (tag.Equals("Item"))
        {
            guiText.text = itemLost;
        }
        // �_�ŊJ�n
        SendMessage("OnStartTextBlink");
    }


    void OnEndTextBlink()
    {
        guiText.enabled = false;
    }
}
