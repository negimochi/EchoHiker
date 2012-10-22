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
        StartCoroutine("Wait");
    }

    void OnGameOver( )
    {
        guiText.text = gameoverText;
        StartCoroutine("Wait");
    }

    void OnStageReset()
    {
        guiText.enabled = false;
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(backtitleDelay);
        BroadcastMessage("OnStartSwitcher");
    }

}
