using UnityEngine;
using System.Collections;

/// <summary>
/// ユーザクリック後、Adapterにシーン終了を伝える
/// </summary>
public class TitleSwitcher : MonoBehaviour {

    [SerializeField]
    private float fadeTime = 3.0f;
    [SerializeField]
    private float delay = 0.2f;
    [SerializeField]
    private float minAlpha = 0.0f;

    private float max;
    private float currentTime = 0.0f;
    [SerializeField]
    private Color startColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private bool wait = true;

    private bool pushed = false;
   
    void Start()
    {
        max = 1.0f - minAlpha;
        //startColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, guiText.material.color.a);
    }

    void Update()
    {
        if (!guiText.enabled) return;

        if (!wait)
        {
            float time = currentTime / fadeTime;
            if (time <= (2.0f * max))
            {
                float alpha = minAlpha + Mathf.PingPong(time, max);
                guiText.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // 時間更新
                currentTime += Time.deltaTime;
            }
            else
            {
                wait = true;
                StartCoroutine("Delay", delay);
            }
        }

        if ( !pushed && Input.GetMouseButtonDown(0))
        {
            pushed = true;
            audio.Play();
            // シーン終了を伝える
            GameObject adapter = GameObject.Find("/Adapter");
            if (adapter) adapter.SendMessage("OnSceneEnd");
            else Debug.Log("adapter is not exist...");
            //intermission.SendMessage("OnIntermissionStart", true);
        }
	}

    private void StartFade()
    {
        guiText.enabled = true;
        guiText.material.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        wait = false;
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
        currentTime = 0.0f;
    }

    void OnStartSwitcher()
    {
        StartFade();
    }

    void OnStageReset()
    {
        guiText.enabled = false;
        wait = true;
        pushed = false;
    }
}
