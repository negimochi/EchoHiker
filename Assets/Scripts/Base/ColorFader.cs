using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;
    [SerializeField]
    private bool valid = false;

    private float max;
    private float currentTime;
    private Color startColor;
    private bool isWait;

    void OnHit()
    {
        SetEnable(false);
    }

    public void SetEnable( bool flag )
    {
        renderer.enabled = flag;
    }

	void Start () 
    {
        renderer.enabled = valid;
       
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
//                param *= Mathf.Exp(-3.0f * Time.deltaTime);
//                //	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
//                Color color = new Color(Mathf.Abs(baseColor.r - param), baseColor.g, baseColor.b);
//                renderer.material.color = color;

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
