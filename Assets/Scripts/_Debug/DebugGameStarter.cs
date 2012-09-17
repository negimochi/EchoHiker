using UnityEngine;
using System.Collections;

public class DebugObjectLoader : MonoBehaviour
{
    [SerializeField]
    private int clickCount = 0;
    [SerializeField]
    private Rect rect= new Rect(0, 70, 100, 20);
    [SerializeField]
    private string targetSceeneName = "Test_SceneLoad";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
