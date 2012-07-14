using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private float offsetPixelY = 0.0f;  // ゼロで画面端
    [SerializeField]
    private int disitSize = 6;

    private int score = 0;

    void Start() 
    {
        // 位置調整
        float h = (float)Screen.height;
        float yPos = 1.0f - offsetPixelY/h;
        transform.position = new Vector3(0.5f, yPos, 0.0f);
    }

    /// <summary>
    /// [BroadcastMessage]スコア取得
    /// </summary>
    /// <param name="value">獲得したスコア</param>
    void OnGetScore( int value )
    {
        score += value;
        guiText.text = score.ToString("D" + disitSize);
    }

    public int Score() { return score; }
}
