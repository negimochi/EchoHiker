using UnityEngine;
using System.Collections;

public class SonarEffect : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float stepStart = 0.0f;
    [SerializeField]
    private float stepEnd = 1.0f;

    private float currentTime;
    private GUITexture texture;
    private Rect baseRect;

    public void Init( Rect rect )
    {
        currentTime = 0.0f;
        baseRect = rect;
        texture = GetComponent<GUITexture>();
        texture.pixelInset = new Rect(baseRect);
        texture.enabled = true;
    }
	
	void FixedUpdate () 
    {
        if (texture.enabled)
        {
            float time = currentTime / duration;
            if (time <= 1.0f)
            {
                float alpha = Mathf.SmoothStep(stepStart, stepEnd, time);
                float w = baseRect.width * (1.0f - alpha);
                float h = baseRect.height * (1.0f - alpha);
                texture.pixelInset = new Rect(baseRect.center.x - w * 0.5f, baseRect.center.y - h * 0.5f, w, h);
                texture.color = new Color(texture.color.r, texture.color.g, texture.color.b, alpha);
                // ŽžŠÔXV
                currentTime += Time.deltaTime;
            }
            else 
            {
                texture.enabled = false;
                StartCoroutine("Delay", delay);
            }
        }
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        texture.enabled = true;
        currentTime = 0.0f;
    }
}
