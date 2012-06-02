using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GUITexture guiSonar = GetComponent<GUITexture>();
        guiSonar.pixelInset = new Rect(20, Screen.height - 260, 240, 240);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
