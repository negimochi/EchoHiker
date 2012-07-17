using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    [SerializeField]
    private float scoreMax = 500;
    [SerializeField]
    private float scoreMin = 10;

    private int scoreValue;

    private GameObject uiObj = null;
    private EnemyCaution caution = null;

    void Start() 
    {
        uiObj = GameObject.Find("/UI");
        caution = GetComponent<EnemyCaution>(); 
    }

    private void OnGetScore()
    {
        Debug.Log("OnGetScore");

        float time = 1.0f - Mathf.InverseLerp(0, 100, caution.Value());
        scoreValue = (int)Mathf.Lerp(scoreMin, scoreMax, time);

        // �X�R�A�l�𑗂�
        uiObj.BroadcastMessage("OnGetScore", scoreValue);
        // �����Ƀq�b�g����
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }
}
