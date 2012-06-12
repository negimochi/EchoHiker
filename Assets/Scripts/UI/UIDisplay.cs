using UnityEngine;
using System.Collections;

public class UIDisplay : MonoBehaviour {

    [SerializeField]
    private GUIStyle style;

    private static string scoreText = "Score: ";
    private static string frameText = "Frame: ";

    public int score = 0;
    public int frame = 1000;

	void Start () 
    {
        ;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.5f, 10.0f, 120.0f, 20.0f), scoreText + score/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, 10.0f, 120.0f, 20.0f), frameText + frame/*, style*/);
    }

    void OnHitItem(GameObject histObj)
    {
        ItemCollider itemCollider = histObj.GetComponent<ItemCollider>();
        if( itemCollider != null )  score += itemCollider.score;
    }
}
