using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

//    [SerializeField]
//    private string easySceneName    = "Main";
//    [SerializeField]
//    private string normalSceneName  = "Main";
//    [SerializeField]
//    private string endlessSceneName = "Main";

//    private GUILayer layer = null;
//    private GameObject intermission = null;

    [SerializeField]
    private string nextSceneName = "Main";

    private bool next = false;
    TitleSwitcher switcher = null;

	void Start () 
    {
//        intermission = GameObject.Find("/UI/Intermission");
//        layer = Camera.main.GetComponent<GUILayer>();
        switcher = GetComponentInChildren<TitleSwitcher>();
	}


//	void Update () 
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Debug.Log(Input.mousePosition);
//            audio.Play();
//            nextSceneName = endlessSceneName;
//            intermission.SendMessage("OnIntermissionStart", true);
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
                intermission.SendMessage("OnIntermissionStart", true);
            }
             */
//        }
//	}

    void OnIntermissionEnd()
    {
        if( next ) {
            Debug.Log("Load:"+nextSceneName);
            // 次のシーンをロード
            Application.LoadLevel(nextSceneName);
        }
        else {
            switcher.StartFade();
            next = true;
        }
    }
}
