using UnityEngine;
using System.Collections;

public class StageAdapter : MonoBehaviour {

//    private GameObject player = null;
//    private GameObject objects = null;
    private GameObject root = null;
    private GameObject field = null;
    private GameObject ui = null;
    private bool nextStage = false;
	
    // ここからスタート
    void OnGameStart()
    {
//        player = GameObject.Find("/Field/Player");
//        objects = GameObject.Find("/Field/Object");
        root = GameObject.Find("/Root");
        field = GameObject.Find("/Field");
        ui = GameObject.Find("/UI");
        // ゲーム開始
//        if (player)  player.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
//        if (objects) objects.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (field) field.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (ui) ui.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
    }


//    void OnNotifyGameEnd( bool nextStage )
    // シーン終了時に呼ばれる
    void OnGameEnd(bool nextStage_)
    {
        nextStage = nextStage_;
        if (nextStage)
        {
            // 次のStage
            //if (root) root.SendMessage("OnNextStage");
            // ステージクリアの挙動指示
	        //BroadcastMessage("OnGameClear", SendMessageOptions.DontRequireReceiver);
            if (field) field.BroadcastMessage("OnGameClear", SendMessageOptions.DontRequireReceiver);
            if (ui) ui.BroadcastMessage("OnGameClear", SendMessageOptions.DontRequireReceiver);
        }
		else {
	        // ゲームオーバーの挙動指示
//	        if (player)  player.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
//	        if (objects) objects.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
            if (field) field.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
            if (ui) ui.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
		}
    }

    void OnSceneEnd()
    {
        // TitleSwitcherからよばれる
        if (nextStage)
        {
            if (root) root.SendMessage("OnNextStage");
        }
        else
        {
            if (root) root.SendMessage("OnStartTitle");
        }
    }

}
