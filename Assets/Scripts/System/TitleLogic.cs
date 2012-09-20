using UnityEngine;
using System.Collections;

public class TitleLogic : MonoBehaviour {

    [SerializeField]
    private SceneSelector.Type next = SceneSelector.Type.Stage1;

    private GameObject root = null;
    private GameObject ui = null;

	void Start () 
    {
        root = GameObject.Find("/Root");
        ui = GameObject.Find("/UI");
    }

    // ここからスタート
    void OnGameStart()
    {
        if(ui) ui.BroadcastMessage("OnStartSwitcher");
    }

    // シーン終了時に呼ばれる
    void OnSceneEnd()
    {
        // Stage1をはじめる
        if (root) root.SendMessage("OnNextStage", next);
    }
}
