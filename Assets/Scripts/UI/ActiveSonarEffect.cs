using UnityEngine;
using System.Collections;

public class ActiveSonarEffect : MonoBehaviour {

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
	
	void FixedUpdate () {
        /*
        counter += Time.deltaTime;

        if (counter > updateTime)
        {
            counter = 0;
            effect.pixelInset = new Rect(sonar.center.x, sonar.center.y, 0, 0);
        }
        else {
            float ratio = counter/updateTime;
            float newWidth  = sonar.width * ratio;
            float newHeight = sonar.height * ratio;
            effect.pixelInset = new Rect( sonar.center.x-newWidth/2.0f, sonar.center.y-newHeight/2.0f, newWidth, newHeight );
            effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, 1.0f - ratio);
        }
         */
	}
}
