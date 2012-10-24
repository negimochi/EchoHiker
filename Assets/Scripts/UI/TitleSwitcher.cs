using UnityEngine;
using System.Collections;

/// <summary>
/// ユーザクリック後、Adapterにシーン終了を伝える
/// </summary>
public class TitleSwitcher : MonoBehaviour {

    [SerializeField]
    private float waitTime = 3.0f;

    private bool pushed = false;
    private bool fade = false;
   
    void Start()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
    }

    void Update()
    {
        if (!guiText.enabled) return;

        if ( !pushed && Input.GetMouseButtonDown(0))
        {
            pushed = true;
            audio.Play();
            // シーン終了を伝える
            GameObject adapter = GameObject.Find("/Adapter");
            if (adapter) adapter.SendMessage("OnSceneEnd");
            else Debug.Log("adapter is not exist...");
        }
	}

    // フェード終了時に呼ばれる
    void OnEndTextFade()
    {
        if (!guiText.enabled) return;
        StartCoroutine("Delay");
    }

    // スイッチのスタート
    void OnStartSwitcher()
    {
        Debug.Log("OnStartSwitcher");
        guiText.enabled = true;
        fade = true;
        SendMessage("OnTextFadeIn");
    }
    // ステージリセット
    void OnStageReset()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
        pushed = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        // FadeInとFadeOutを切り替えて実行
        fade = !fade;
        if (fade) SendMessage("OnTextFadeIn");
        else SendMessage("OnTextFadeOut");
    }

}
