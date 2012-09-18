using UnityEngine;
using System.Collections;

public class DebugSceneSelector : MonoBehaviour
{
    [SerializeField]
    private Rect rectArea = new Rect(0, 0, 640, 480);
    [SerializeField]
    private Rect rectT = new Rect(0, 430, 100, 20);
    [SerializeField]
    private Rect rectS1 = new Rect(100, 430, 100, 20);
    [SerializeField]
    private Rect rectS2 = new Rect(200, 430, 100, 20);
    [SerializeField]
    private Rect rectS3 = new Rect(300, 430, 100, 20);

    private GameObject root = null;

	// Use this for initialization
	void Start () {
        root = GameObject.Find("/Root");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnIntermissionStart( IntermissionEffector.Type type)
    {
        switch(type)
        {
            case IntermissionEffector.Type.SlideIn: break;
            case IntermissionEffector.Type.SlideOut:    break;
            default:    break;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(rectArea);
        if (GUI.Button(rectT, "Load Title"))
        {
            if (root) root.SendMessage("OnStartTitle");
        }
        if (GUI.Button(rectS1, "Load Stage1"))
        {
            if (root) root.SendMessage("OnNextStage", SceneSelector.Type.Stage1);
        }
        if (GUI.Button(rectS2, "Load Stage2"))
        {
            if (root) root.SendMessage("OnNextStage", SceneSelector.Type.Stage2);
        }
        if (GUI.Button(rectS3, "Load Stage3"))
        {
            if (root) root.SendMessage("OnNextStage", SceneSelector.Type.Stage3);
        }
        GUILayout.EndArea();
    }
}
