using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject player = null;
    private GameObject enemy = null;
    private GameObject item = null;
    private bool gameover = false;

    [SerializeField]
    private string nextSceneName = "Title";

    void Start() 
    {
        Debug.Log("GameStart!!");
        player = GameObject.Find("/Player");
        enemy = GameObject.Find("/Object/EnemyManager");
        item  = GameObject.Find("/Object/ItemManager");
    }
	
    void OnIntermissionEnd()
    {
        if (gameover)
        {
            // タイトルに戻る
            Application.LoadLevel(nextSceneName);
        }
        else
        {
            // ゲームスタート
            if (player) player.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
            if (enemy) enemy.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
            if (item) item.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
            BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        }
    }


    void OnNotifyGameEnd()
    { 
        // 終了通知を送る
        if (player) player.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        if (enemy) enemy.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        if (item) item.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        gameover = true;
    }

}
