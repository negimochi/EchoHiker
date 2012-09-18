using UnityEngine;
using System.Collections;

public class TitleSwitcher : MonoBehaviour {

    [SerializeField]
    private float fadeTime = 3.0f;
    [SerializeField]
    private float delay = 0.2f;
    [SerializeField]
    private float minAlpha = 0.0f;

    private float max;
    private float currentTime = 0.0f;
    private Color startColor;
    private bool wait = true;

    private bool pushed = false;

    private GameObject logic = null;
//   private GameObject intermission = null;
   
    void Start()
    {
        max = 1.0f - minAlpha;
        startColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b);
        logic = GameObject.Find("/Logic");
//        intermission = GameObject.Find("/UI/Intermission");
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
                Debug.Log(guiText.material.color.a);
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
            if (logic) logic.SendMessage("OnSceneEnd");
            else Debug.Log("field is not exist...");
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
}
