using UnityEngine;
using System.Collections;

public class SonarMessage : MonoBehaviour {

    [SerializeField]
    private string enemyCreated = "The enemy is destroyed!";
    [SerializeField]
    private string enemyCaution = "The enemy is destroyed!";
    [SerializeField]
    private string enemyHit = "The enemy is destroyed!";

    [SerializeField]
    private string itemCleated = "The item is lost...";
    [SerializeField]
    private string itemHit = "The item is lost...";
    [SerializeField]
    private string itemLost = "The item is lost...";

    void Start() 
    {
        guiText.text = "";
	}


    void OnDestory()
    { 
    }
}
