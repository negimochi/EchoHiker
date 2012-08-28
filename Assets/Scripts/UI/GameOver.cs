using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private float backtitleDelay = 3.0f;


    void OnGameOver( )
    {
        guiText.enabled = true;
        StartCoroutine("Wait", backtitleDelay);
    }

    
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        TitleSwitcher titleback = GetComponentInChildren<TitleSwitcher>();
        titleback.StartFade();
    }

}
