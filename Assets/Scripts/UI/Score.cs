using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    [SerializeField]
    private int disitSize = 6;

    private const string fixedText = "SCORE ";
    private int scoreValue = 0;

    void Start() 
    {
        guiText.pixelOffset = new Vector2( Screen.width * 0.5f, Screen.height - 32.0f );
	}

    void OnGetScore( int value )
    {
        scoreValue += value;
        guiText.text = fixedText + scoreValue.ToString("D" + disitSize);
    }

    public int Value() { return scoreValue; }
}
