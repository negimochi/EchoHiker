using UnityEngine;
using System.Collections;

public class StageEndText : MonoBehaviour {

    [SerializeField]
    private float backtitleDelay = 3.0f;
    [SerializeField]
    private string gameclearText = "STAGE CLEAR";
    [SerializeField]
    private string gameoverText = "GAME OVER";

    void Start()
    {
    }

    void OnGameClear()
    {
        guiText.text = gameclearText;
        guiText.enabled = true;
        StartCoroutine("Wait", backtitleDelay);
    }

    void OnGameOver( )
    {
        guiText.text = gameoverText;
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
