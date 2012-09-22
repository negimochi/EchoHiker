using UnityEngine;
using System.Collections;

public class StageEndText : MonoBehaviour {

    [SerializeField]
    private float backtitleDelay = 3.0f;

    void OnGameClear()
    {
        guiText.text = "GAME CLEAR";
        guiText.enabled = true;
        StartCoroutine("Wait", backtitleDelay);
    }

    void OnGameOver( )
    {
        guiText.text = "GAME OVER";
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
