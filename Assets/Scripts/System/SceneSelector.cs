using UnityEngine;
using System.Collections;

public class SceneSelector : MonoBehaviour {

    public enum Type {
        None = -1,
        Title = 0,
        Stage1,
        Stage2,
        Stage3
    };
    [SerializeField]
    private string[] mainSceneName = new string[] { 
        "Title",
        "Stage1", "Stage2", "Stage3" 
    };  // 各種ステージ
    [SerializeField]
    private string stageuiSceneName = "StageUI";
    [SerializeField]
    private Type currentType = Type.None;
//    [SerializeField]
//    private Type prevType = Type.None;
    [SerializeField]
    private bool playOnAwake = true;

    private GameObject logic = null;
    private GameObject ui = null;
    //    private GameObject field = null;

    [SerializeField]
    private int hiScore = 0;
    private bool loaded = false;

	void Awake()
	{
		// OnLoadでDestory対象からはずす
        DontDestroyOnLoad(gameObject);
	}
	
    void Start() 
    {
        if (playOnAwake) StartCoroutine("Wait", 1.0f);
    }
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnStartTitle();
    }

    bool Load()
    {
        if (currentType == Type.None) return false;
        int index = (int)currentType;

        if (ui)
        {
            // Titleに戻る場合は事前にuiを削除しておく
            if (currentType == Type.Title)
            {
                // ハイスコア更新
                int newscore = 0;
                StageUI stageUI = ui.GetComponent<StageUI>();
                if (stageUI) newscore = stageUI.Score();
                if (hiScore < newscore) hiScore = newscore;
                // 削除
                Destroy(ui);
            }
        }

        Application.LoadLevel(mainSceneName[index]);       // フィールドシーンロード
        return true;
    }

    // ロード終了時に
    void OnLevelWasLoaded( int level )
    {
        Debug.Log("OnLevelWasLoaded : level=" + level + " - " + Application.loadedLevelName);
        // 参照を保存
        InitReference();
        if (loaded)
        {
            if (ui == null)
            {
                // StageだったらStageUIを追加ロード
                switch (currentType)
                {
                    default: break;
                    case Type.Stage1:
                    case Type.Stage2:
                    case Type.Stage3:
                        Application.LoadLevelAdditive(stageuiSceneName); break;
                }
            }
            else {
                // スコア以外をリセット
                ui.BroadcastMessage("OnStageReset", SendMessageOptions.DontRequireReceiver);
            }

            // インターミッション
            BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideOut);
        }
    }

    void InitReference()
    {
        ui = GameObject.Find("/UI");
        logic = GameObject.Find("/Logic");
        if (logic)
        {
            // logicさえあればロードできたとみなす
            loaded = true;
        }
    }

    // タイトルをロード
    void OnStartTitle()
    {
        loaded = false;
        //prevType = currentType;

        // タイトルを設定
        currentType = Type.Title;
        
        // インターミッション開始
        BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideIn);
    }

    // 次のステージをロード
    void OnNextStage( Type setType=Type.None )
    {
        loaded = false;
        //prevType = currentType;

        // currentTypeを設定する
        if (setType == Type.None)
        {
            int current = (int)currentType;
            current++;
            // ステージ数をオーバーしていた場合はTitleへ戻す
            if (current >= mainSceneName.Length) currentType = Type.Title;
            else currentType = (Type)(current);
        }
        else currentType = setType;
        
        // インターミッション開始
        BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideIn);
    }

    // インターミッションの終了受け取り
    void OnIntermissionEnd()
    {
        if (loaded) logic.SendMessage("OnGameStart");   // ロード済みならゲームスタート
        else Load();        // ロードできてないならロード開始
    }
}
