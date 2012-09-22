using UnityEngine;
using System.Collections;

public class TitleAdapter : MonoBehaviour {

    private GameObject root = null;
    private GameObject ui = null;

    // ロード終了時
//    void OnLevelWasLoaded(int level)
//    {
//        Debug.Log("OnLevelWasLoaded : level=" + level + " - " + Application.loadedLevelName);
    // ロード終了時にすると、単体デバッグができないのでStartにしておく
    void Start()
    {
        root = GameObject.Find("/Root");
        ui = GameObject.Find("/UI");

        if (root) root.BroadcastMessage("OnFadeOut", gameObject);
        else OnIntermissionEnd();
    }

    // インターミッションの終了受け取り
    void OnIntermissionEnd()
    {
        // ここからゲームスタート(プレイヤー操作可能)
        if(ui) ui.BroadcastMessage("OnStartSwitcher");
    }

    // シーン終了時に呼ばれる
    void OnSceneEnd()
    {
        // Stageをはじめる
        if (root) root.SendMessage("OnStartStage");
    }
}
