using UnityEngine;
using System.Collections;

public class DebugCounterGUI : MonoBehaviour
{
    [SerializeField]
    private Rect upperRect= new Rect(0, 0, 100, 20);
    [SerializeField]
    private Rect downRect = new Rect(0, 30, 100, 20);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        if (GUI.Button(upperRect, "Count Up"))
        {
            SendMessage("OnGetScore", 10);
        }
        if (GUI.Button(downRect, "Count Down"))
        {
            SendMessage("OnGetScore", -10);
        }
        GUILayout.EndArea();
    }
}
