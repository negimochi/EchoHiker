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

    [SerializeField]
    private float temetuTime = 2.0f;
    [SerializeField]
    private float displayTime = 2.0f;
    [SerializeField]
    private float fadeTime = 3.0f;

    void Start() 
    {
        guiText.enabled = false;
	}


    void OnEndItemLifetime()
    {
        guiText.text = itemLost;
        guiText.enabled = true;
        StartCoroutine("Delay", displayTime);
    }

    private IEnumerator Delay(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
    }
}
