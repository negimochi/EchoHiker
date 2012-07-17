using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

//    [SerializeField]
//    private string easySceneName    = "Main";
//    [SerializeField]
//    private string normalSceneName  = "Main";
    [SerializeField]
    private string endlessSceneName = "Main";

//    private GUILayer layer = null;
    private GameObject intermission = null;

    private string nextScene;

	void Start () 
    {
        intermission = GameObject.Find("/UI/Intermission");
//        layer = Camera.main.GetComponent<GUILayer>();
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            nextScene = endlessSceneName;
            intermission.SendMessage("OnIntermissionStart");
            /*
            GUIElement element = layer.HitTest(Input.mousePosition);
            if (element)
            {
                Debug.Log(element.name);
                switch (element.name)
                {
                    default: return;
                    case "MenuText_Easy":   nextScene = easySceneName; break;
                    case "MenuText_Normal": nextScene = normalSceneName; break;
                    case "MenuText_Endless": nextScene = endlessSceneName; break;
                }
                intermission.SendMessage("OnIntermissionStart");
            }
             */
        }
	}

    void OnIntermissionEnd()
    {
        Debug.Log("LoadLevel");
        // 次のシーンをロード
        Application.LoadLevel(nextScene);
    }
}
