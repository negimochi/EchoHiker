using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;

    private float max;
    private float currentTime;
    private Color startColor;
    private bool wait;
    private bool sonarHit;
    private bool sonarInside;

	void Start () 
    {
        sonarHit = false;
        sonarInside = false;
        wait = false;
        max = 1.0f - minAlpha;
        currentTime = 0.0f;
        startColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a);
	}

	void Update () 
    {
        if (!wait)
        {
            float time = currentTime / duration;
            if (time <= (2.0f*max))
            {
                float alpha = 1.0f - Mathf.PingPong(time, max);
                renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // 時間更新
                currentTime += Time.deltaTime;
            }
            else
            {
                wait = true;
                StartCoroutine("Delay", delay);
            }
        }
	}

    /// <summary>
    /// [SendBroadcastMessage]
    /// </summary>
    void OnHit()
    {
        // ヒットした瞬間でソナーから見えなくする
        sonarHit = false;
        Enable();
    }

    void OnSonar()
    {
        // ソナーから見えることを許可する
        sonarHit = true;
        Enable();
    }

    void OnSonarInside()
    {
        sonarInside = true;
        Enable();
    }
    void OnSonarOutside()
    {
        sonarInside = false;
        Enable();
    }

    private void Enable()
    {
        renderer.enabled = sonarInside & sonarHit;
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
        currentTime = 0.0f;
    }

}
