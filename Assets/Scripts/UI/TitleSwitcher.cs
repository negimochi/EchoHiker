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

    private GameObject intermission = null;
   
    void Start()
    {
        max = 1.0f - minAlpha;
        startColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b);
        intermission = GameObject.Find("/UI/Intermission");
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
                // ŽžŠÔXV
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
            intermission.SendMessage("OnIntermissionStart", true);
        }
	}

    public void StartFade()
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
}
