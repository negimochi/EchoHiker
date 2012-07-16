using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject player = null;
    private GameObject enemy = null;
    private GameObject item = null;
    private bool start;

    void Start() 
    {
        player = GameObject.Find("/Player");
        enemy = GameObject.Find("/Object/EnemyManager");
        item = GameObject.Find("/Object/ItemManager");
    }
	
	void Update () 
    {
	
	}


    void OnIntermissionEnd()
    {
        // Intermission�̏I���B�Q�[���X�^�[�g
        start = true;
        if (player) player.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (enemy) enemy.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        if (item) item.SendMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
    }


    void OnNotifyGameEnd()
    { 
        // �I���ʒm�𑗂�
        start = false;
        if (player) player.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Player End");
        if (enemy) enemy.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Enemy End");
        if (item) item.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Item End");
        BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
    }

    // �O�̂��߈�����悤�ɂ��Ƃ�
    public bool IsGameStart() { return start; }
}
