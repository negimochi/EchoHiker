using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;

    private float max;
    private float currentTime;
    private Color startColor;
    private bool isWait;

	void Start () 
    {
        max = 1.0f - minAlpha;
        isWait = false;
        currentTime = 0.0f;
        startColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a);
	}
	
	void Update () 
    {
        if (!isWait)
        {
            float time = currentTime / duration;
            if (time <= (2.0f*max))
            {
                //float alpha = Mathf.SmoothStep(0.0f, 1.0f, time);
                float alpha = 1.0f - Mathf.PingPong(time, max);
                renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // ŽžŠÔXV
                currentTime += Time.deltaTime;
            }
            else
            {
                isWait = true;
                StartCoroutine("Delay", delay);
            }
        }
	}

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
        currentTime = 0.0f;
    }
}
