using UnityEngine;
using System.Collections;

public class StageStartText : MonoBehaviour {

    [SerializeField]
    private float waitTime = 1.0f;
    [SerializeField]
    private float fadeTime = 3.0f;
    [SerializeField]
    private string[] startText = new string[] { 
        "Stage 1", "Stage 2", "Final Stage" 
    };
    [SerializeField]
    private string[] missionText = new string[] { 
        "Kill the Enemy!", 
        "Get the Recovery Item!", 
        "As long as" 
    };

    private float currentTime = 0.0f;
    private bool wait = false;
    private GUIText missionGUIText = null;
    private Color startColor;

    void Start()
    {
        GameObject missionObj = GameObject.Find("MissionText");
        missionGUIText = missionObj.guiText;
        startColor = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, guiText.material.color.a);
    }

    void Update()
    {
        if (!guiText.enabled) return;

        if (!wait)
        {
            float timeRate = currentTime / fadeTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, timeRate);
            guiText.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            missionGUIText.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            currentTime += Time.deltaTime;

            if (timeRate >= 1.0f)
            {
                wait = true;
                guiText.enabled = false;
                missionGUIText.enabled = false;
            }
        }
    }

    void OnAwakeStage(int index)
    {
        if (index >= startText.Length) return;
        guiText.text = startText[index];
        guiText.enabled = true;
        guiText.material.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        missionGUIText.text = missionText[index];
        missionGUIText.enabled = true;
        missionGUIText.material.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        wait = true;
    }

    void OnGameStart()
    {
        currentTime = 0.0f;
        //wait = false;
        StartCoroutine("Delay");
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }

}
