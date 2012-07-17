using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;
//    [SerializeField]
//    private bool valid = false;

    private float max;
    private float currentTime;
    private Color startColor;
    private bool isWait;

	void Start () 
    {
        //renderer.enabled = valid;
       
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
                float alpha = 1.0f - Mathf.PingPong(time, max);
                renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // 時間更新
                currentTime += Time.deltaTime;
            }
            else
            {
                isWait = true;
                StartCoroutine("Delay", delay);
            }
        }
	}

    /// <summary>
    /// [SendBroadcastMessage]
    /// </summary>
    void OnHit()
    {
        // ソナーから見えなくする
        SetEnable(false);
    }

    void OnSonar()
    {
        SetEnable(true);
    }

    /// <summary>
    /// 表示・非表示切り替え
    /// </summary>
    /// <param name="flag"></param>
    public void SetEnable(bool flag)
    {
        renderer.enabled = flag;
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
        currentTime = 0.0f;
    }
}
