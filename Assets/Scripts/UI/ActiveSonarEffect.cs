using UnityEngine;
using System.Collections;

public class ActiveSonarEffect : MonoBehaviour {

    [SerializeField]
    private float updateTime = 1.0f;
    [SerializeField]
    private float delay = 1.0f;


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
        currentTime += Time.deltaTime;

        if (texture.enabled)
        {
            float alpha = Mathf.SmoothStep(1.0f, 0.0f, currentTime / updateTime);
            float w = baseRect.width * (1.0f-alpha);
            float h = baseRect.height * (1.0f-alpha);

            texture.pixelInset = new Rect(baseRect.center.x - w * 0.5f, baseRect.center.y - h * 0.5f, w, h);
            texture.color = new Color(texture.color.r, texture.color.g, texture.color.b, alpha);

            if (currentTime > updateTime)
            {
                currentTime = 0.0f;
                texture.enabled = false;
            }
        }
        else
        {
            if (currentTime > delay)
            {
                currentTime = 0.0f;
                texture.enabled = true;
            }
        }
    }
}
