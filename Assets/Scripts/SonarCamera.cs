using UnityEngine;
using System.Collections;

public class SonarCamera : MonoBehaviour {

	void Start () 
    {
	}

	public void SetRect( Rect rect )
    {
        //Camera sonarCamera = gameObject.GetComponent<Camera>();

        float r = rect.width * 0.5f;
        float newWidth = r * Mathf.Pow(2.0f,0.5f);   // “àÚ‚·‚é³•ûŒ`‚Ìˆê•Ó
        float sub = (rect.width - newWidth)*0.5f;
        camera.pixelRect = new Rect(rect.x + sub, rect.y + sub, newWidth, newWidth);
        
        //sonarCamera.pixelRect = new Rect( rect );
    }
}
