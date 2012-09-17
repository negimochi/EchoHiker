using UnityEngine;
<<<<<<< HEAD
using System.Collections;

public class DebugSonarGUI : MonoBehaviour
{

    [SerializeField]
=======
using System.Collections;

public class DebugSonarGUI : MonoBehaviour
{

    [SerializeField]
>>>>>>> origin/master
    private Rect rect = new Rect(0,0,10,200);
    [SerializeField]
    private bool enableToggle = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
<<<<<<< HEAD
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        enableToggle = GUI.Toggle(rect, enableToggle, "Sonar Enable(Enemy):");
        renderer.enabled = enableToggle;
=======
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        enableToggle = GUI.Toggle(rect, enableToggle, "Sonar Enable(Enemy):");
        renderer.enabled = enableToggle;
>>>>>>> origin/master
        GUILayout.EndArea();
    }
}
