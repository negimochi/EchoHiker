using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject root = null;
    private GameObject player = null;
    private GameObject objects = null;
    private GameObject ui = null;
//    private bool gameover = false;
	
    void Start() 
    {
    }

    // ここからスタート
    void OnGameStart()
    {
        root = GameObject.Find("/Root");
        player = GameObject.Find("/Field/Player");
        objects = GameObject.Find("/Field/Object");
        ui = GameObject.Find("/UI");
        // ゲーム開始
        if (player)  player.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (objects) objects.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (ui) ui.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
    }


//    void OnNotifyGameEnd( bool nextStage )
    // シーン終了時に呼ばれる
    void OnGameEnd(bool nextStage)
    { 
		if(nextStage) {
            // 次のStage
            if (root) root.SendMessage("OnNextStage");
            // ステージクリア通知
	        //BroadcastMessage("OnGameClear", SendMessageOptions.DontRequireReceiver);
		}
		else {
	        // 終了通知を送る
	        if (player)  player.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
	        if (objects) objects.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
            if (ui) ui.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
	        // ゲームオーバーを記録
//	        gameover = true;
		}
    }

}
