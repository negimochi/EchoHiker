using UnityEngine;
using System.Collections;

public class SonarMessage : MonoBehaviour {

    [SerializeField]
    private string enemyDestroyed = "The enemy is destroyed!";
    [SerializeField]
    private string itemFound = "You found the item!";
    [SerializeField]
    private string itemLost = "The item is lost...";

    [SerializeField]
    private float flashTime = 2.0f;
    [SerializeField]
    private int num = 6;

    private int count = 0;

    void Start() 
    {
        guiText.enabled = false;
        guiText.text = "";
   }


    void OnEndEnemyDestroyed()
    {
        guiText.text = enemyDestroyed;
        StartFlash();
    }

    void OnEndItemFound()
    {
        guiText.text = itemFound;
        StartFlash();
    }

    void OnEndItemLifetime()
    {
        guiText.text = itemLost;
        StartFlash();
    }

    private void StartFlash()
    {
        count = 0;
        guiText.enabled = true;
        StartCoroutine("Delay", flashTime);
    }


    private IEnumerator Delay(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        guiText.enabled = !guiText.enabled;
        count ++;
        if (count > num)
        {
            StartCoroutine("Delay", flashTime);
        }
    }
}
