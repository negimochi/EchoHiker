using UnityEngine;
using System.Collections;

public class UIDisplay : MonoBehaviour {

    [SerializeField]
    private GUIStyle style;

    private static string scoreText = "Score: ";
    private static string frameText = "Frame: ";
    private static string cautionText = "Caution: ";

    public int score = 0;
    public int frame = 1000;
    public int caution = 0;

	void Start () 
    {
        ;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.5f, 10.0f, 120.0f, 20.0f), scoreText + score/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, 10.0f, 120.0f, 20.0f), frameText + frame/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, 120.0f, 20.0f), cautionText + caution.ToString() + "%");
    }

    void OnGetItem( int value )
    {
        score += value;
    }

    void OnDestroyEnemy( int value )
    {
        score += value;
    }

    void OnDamege( int value )
    {
        frame -= value;
        if (frame < 0) {
            frame = 0;
            // I—¹’Ê’m‚ð‘—‚é
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) player.BroadcastMessage("OnGameOver");
            GameObject obj = GameObject.Find("/Object");
            if (obj) obj.BroadcastMessage("OnGameOver");
        }
    }

    void OnUpdateCaution( int value )
    {
        caution = value;
    }
}
