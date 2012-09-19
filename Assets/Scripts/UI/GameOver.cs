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

    void OnStageReset()
    {
        guiText.enabled = false;
    }
    
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        BroadcastMessage("OnStartSwitcher", SendMessageOptions.DontRequireReceiver);
    }

}
