using UnityEngine;
using System.Collections;

public class DebugSceneChanger : MonoBehaviour
{
    [SerializeField]
    private int clickCount = 0;
    [SerializeField]
    private Rect rect = new Rect(0, 70, 100, 20);
    [SerializeField]
    private string targetSceeneName = "Test_SceneLoad";

    [SerializeField]
    private bool dontDestory = true;

    void Awake()
    {
        // 自分のオブジェクトを再ロードしない
        if(dontDestory)DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        if (GUI.Button(rect, "Scene Load"))
        {
            clickCount++;
            // ゲームオーバーだったら、タイトルに戻る
            Application.LoadLevel(targetSceeneName);
        }
        GUILayout.EndArea();
    }
}
