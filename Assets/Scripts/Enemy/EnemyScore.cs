using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    [SerializeField]
    private int scoreMax = 500;
    [SerializeField]
    public int scoreMin = 10;

    private int scoreValue = 100;

    private GameObject uiObj = null;

    void Start() 
    {
        uiObj = GameObject.Find("/UI");
    }

    private void OnGetScore()
    {
        Debug.Log("OnGetScore");
        // スコア値を送る
        uiObj.BroadcastMessage("OnGetScore", scoreValue);

        // 自分にヒット判定
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }
}
