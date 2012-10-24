using UnityEngine;
using System.Collections;

/// <summary>
/// テキストのフェード
/// </summary>
public class TextFader : MonoBehaviour {

    [SerializeField]
    private float waitTime = 0.05f;
    [SerializeField]
    private float fadeTime = 3.0f;
    [SerializeField]
    private float maxAlpha = 1.0f;
    [SerializeField]
    private float minAlpha = 0.0f;

    private float fromValue = 0.0f;
    private float toValue   = 1.0f;
    private Color baseColor;

	void Start () 
    {
        if (guiText) baseColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, guiText.material.color.a);
	}

    // フェードアウトをスタート
    void OnTextFadeOut()
    {
        if (!guiText) return;
        Debug.Log("OnTextFadeOut");
        fromValue = maxAlpha;
        toValue = minAlpha;
        // コルーチンでフェード処理
        StartCoroutine("Fade", fadeTime);
    }

    // フェードインをスタート
    void OnTextFadeIn()
    {
        if (!guiText) return;
        Debug.Log("OnTextFadeIn");
        fromValue = minAlpha;
        toValue = maxAlpha;
        // コルーチンでフェード処理
        StartCoroutine("Fade", fadeTime);
    }

    private IEnumerator Fade(float duration)
    {
        // フェード
        float currentTime = 0.0f;
        while (duration > currentTime)
        {
            float alpha = Mathf.Lerp(fromValue, toValue, currentTime/duration);
            guiText.material.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            // 時間更新
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }
        // フェード終了通知
        SendMessage("OnEndTextFade", SendMessageOptions.DontRequireReceiver);
    }
}
