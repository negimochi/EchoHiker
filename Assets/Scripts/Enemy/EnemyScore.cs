using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    [SerializeField]
    private int scoreMax = 500;
    [SerializeField]
    public int scoreMin = 10;

    private int scoreValue = 100;

    private GameObject ui = null;

    void Start() 
    {
        ui = GameObject.Find("/UI");
    }

    private void OnGetScore()
    {
        Debug.Log("OnGetScore");
        // スコア値を送る
        ui.BroadcastMessage("OnGetScore", scoreValue);

        // 自分にヒット判定
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }
}
