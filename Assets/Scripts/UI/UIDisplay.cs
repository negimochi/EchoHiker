using UnityEngine;
using System.Collections;

public class UIDisplay : MonoBehaviour {

//    [SerializeField]
//    private GUIStyle style;

    private static string scoreText = "Score ";
    private int score;

	void Start () {
	}

    void OnGUI()
    {
        GUI.Label(new Rect( Screen.width*0.5f-30.0f, 10.0f, 60.0f, 20.0f ), scoreText + score/*, style*/);
	}
}
