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

    // �Q�[���N���A�ʒm
    void OnGameClear()
    {
        guiText.text = gameclearText;
        StartCoroutine("Wait");
    }
    // �Q�[���I�[�o�[�ʒm
    void OnGameOver()
    {
        guiText.text = gameoverText;
        StartCoroutine("Wait");
    }
    // �X�e�[�W���Z�b�g
    void OnStageReset()
    {
        guiText.enabled = false;
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(backtitleDelay);
        guiText.enabled = true;
        BroadcastMessage("OnStartSwitcher");
    }

}
