using UnityEngine;
using System.Collections;

public class SonarEffect : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.0f;
    [SerializeField]
    private float delay = 1.0f;

    enum Type {
        Active,
        Passive
    };
    [SerializeField]
    private Type type = Type.Passive;

    private float stepStart = 0.0f;
    private float stepEnd = 1.0f;

    private float alpha = 0.0f;
    private float currentTime = 0.0f;
    private GUITexture texture;
    private Rect baseRect;

    public void Init( Rect rect )
    {
        baseRect = rect;
        switch (type)
        {
            case Type.Active:
                stepStart = 0.0f;
                stepEnd = 1.0f;
                break;
            case Type.Passive:
                stepStart = 1.0f;
                stepEnd = 0.0f;
                break;
        }

        texture = GetComponent<GUITexture>();
        texture.pixelInset = new Rect(baseRect);
        texture.enabled = true;
    }
	
	void Update () 
    {
        if (texture.enabled)
        {
            float time = currentTime / duration;
            if (time <= 1.0f)
            {
                alpha = Mathf.SmoothStep(stepStart, stepEnd, time);
                float w = baseRect.width * alpha;
                float h = baseRect.height * alpha;
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

    public float Value(){   return alpha;    }

}
