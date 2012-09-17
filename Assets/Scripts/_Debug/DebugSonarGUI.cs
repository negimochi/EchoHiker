using UnityEngine;
using System.Collections;

public class DebugSonarGUI : MonoBehaviour
{
    [SerializeField]
    private Rect rect = new Rect(0,0,10,200);
    [SerializeField]
    private bool enableToggle = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        enableToggle = GUI.Toggle(rect, enableToggle, "Sonar Enable(Enemy):");
        renderer.enabled = enableToggle;
        GUILayout.EndArea();
    }
}
