using UnityEngine;
using System.Collections;

public class PassiveSonarEffect : MonoBehaviour {

    private float updateTime;
    private float counter;
    private GUITexture texture;
    private Rect baseRect;

    public void Init(GUITexture effectTexture, Rect rect, float time)
    {
        baseRect = rect;
        texture = effectTexture;
        updateTime = time;
        counter = 0.0f;
        texture.pixelInset = new Rect(baseRect);
        texture.color = new Color(0.8f, 0.8f, 1.0f, 1.0f);
    }
	
	void Update () {
        counter += Time.deltaTime;

        if (counter > updateTime)
        {
            counter = 0;
            texture.pixelInset = new Rect(baseRect);
        }
        else {
            float ratio = counter/updateTime;
            float w = baseRect.width * ratio;
            float h = baseRect.height * ratio;
            texture.pixelInset = new Rect(sonar.center.x - newWidth / 2.0f, sonar.center.y - newHeight / 2.0f, newWidth, newHeight);
            texture.color = new Color(texture.color.r, texture.color.g, texture.color.b, 1.0f - ratio);
        }
	}
}
