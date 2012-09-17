using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject player = null;
    private GameObject objects = null;
    private bool gameover = false;
	
	private int currentStage = 0;
	
    [SerializeField]
    private string mainSceneName = "Main";
    [SerializeField]
    private string titleSceneName = "Title";
	
	void Awake()
	{
		// UI系はDestoryしないで引き継ぐ
        //DontDestroyOnLoad(gameObject);
	}
	
    void Start() 
    {
        Debug.Log("GameStart!!");
        player = GameObject.Find("/Player");
        objects = GameObject.Find("/Object");
    }
	
    void OnIntermissionEnd()
    {
        // IntermissionEffectorからの終了通知
        if (gameover)
        {
            // ゲームオーバーだったら、タイトルに戻る
            Application.LoadLevel(titleSceneName);
        }
        else
        {
			currentStage++;
			// ゲーム開始
            if (player)  player.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
            if (objects) objects.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
            BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        }
    }


    void OnNotifyGameEnd( bool nextStage )
    { 
		if(nextStage) {
			// ステージクリア通知
	        BroadcastMessage("OnGameClear", SendMessageOptions.DontRequireReceiver);
		}
		else {
	        // 終了通知を送る
	        if (player)  player.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
	        if (objects) objects.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
	        BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
	        // ゲームオーバーを記録
	        gameover = true;
		}
    }

}
