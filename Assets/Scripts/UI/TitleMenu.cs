using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

    private SceneSelector.Type next = SceneSelector.Type.Stage1;
    private GameObject root = null;

	void Start () 
    {
        root = GameObject.Find("/Root");
        BroadcastMessage("OnStartSwitcher");
    }

    // シーン終了時に呼ばれる
    void OnSceneEnd()
    {
        // Stage1をはじめる
        if (root) root.SendMessage("OnNextStage", next);
    }
}
