using UnityEngine;
using System.Collections;

public class SonarEffect : MonoBehaviour {

    [SerializeField]
    private float updateTime;
    private float counter;

    private GUITexture effect;
    private Rect sonar;
	// Use this for initialization
	void Start () 
    {
        counter = 0.0f;
        effect = gameObject.GetComponent<GUITexture>();
        sonar = transform.parent.gameObject.GetComponent<GUITexture>().pixelInset;
        sonar.x = 20;
        sonar.y = Screen.height - 260;
        effect.pixelInset = new Rect( sonar.center.x, sonar.center.y, 0, 0 );
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        counter += Time.fixedDeltaTime;

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
	}
}
