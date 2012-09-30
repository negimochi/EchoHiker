using UnityEngine;
using System.Collections;

public class TextFader : MonoBehaviour {

    [SerializeField]
    private float waitTime = 0.05f;

    private float duration = 0.0f;
    private float fromValue = 0.0f;
    private float toValue   = 1.0f;
    private Color baseColor;

	void Start () 
    {
        if (guiText) baseColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, guiText.material.color.a);
	}

    void OnFadeOut( float fadeTime )
    {
        if (!guiText) return;
        fromValue = baseColor.a;
        toValue   = 0.0f;
        StartCoroutine("Fade", fadeTime);
    }
    void OnFadeIn( float fadeTime )
    {
        if (!guiText) return;
        fromValue = 0.0f;
        toValue   = baseColor.a;
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
