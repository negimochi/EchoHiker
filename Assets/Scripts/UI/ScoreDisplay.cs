using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private float offsetPixelY = 0.0f;  // �[���ŉ�ʒ[
    [SerializeField]
    private int disitSize = 6;

    private int score = 0;

    void Start() 
    {
        // �ʒu����
        float h = (float)Screen.height;
        float yPos = 1.0f - offsetPixelY/h;
        transform.position = new Vector3(0.5f, yPos, 0.0f);
    }

    /// <summary>
    /// [BroadcastMessage]�X�R�A�擾
    /// </summary>
    /// <param name="value">�l�������X�R�A</param>
    void OnGetScore( int value )
    {
        score += value;
        guiText.text = score.ToString("D" + disitSize);
    }

    public int Score() { return score; }
}
