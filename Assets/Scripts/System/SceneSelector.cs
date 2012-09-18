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
    private string stageuiSceneName = "StageUI";    // ステージUI。各ステージ共通
    [SerializeField]
    private Type currntType = Type.None;
    [SerializeField]
    private bool playOnAwake = true;

    private GameObject field = null;
    private GameObject logic = null;
    private GameObject ui = null;

    private int score = 0;
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

    void Load()
    {
        int index = (int)currntType;
        switch( currntType ) {
            case Type.Stage1:
            case Type.Stage2:
            case Type.Stage3:
                Application.LoadLevel(mainSceneName[index]);       // フィールドシーンロード
                break;
            case Type.Title:
                if (ui != null)
                {
                    Debug.Log("Destroy ui");
                    Destroy(ui);    // UIは強制削除
                }
                Application.LoadLevel(mainSceneName[index]);
                break;

            case Type.None:
            default:
                Debug.Log("Field Scene is not exist....");
                break;
        }
    }

    // ロード終了時に
    void OnLevelWasLoaded( int level )
    {
        Debug.Log("OnLevelWasLoaded : level=" + level + " - " + Application.loadedLevelName);
        // 参照を保存
        if (InitReference())
        {
            // インターミッション
            BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideOut);
        }
    }

    bool InitReference()
    {
        field = GameObject.Find("/Field");
        logic = GameObject.Find("/Logic");
        ui = GameObject.Find("/UI");

        if (logic)
        {
            loaded = true;
            Debug.Log("Field Scene is exist!");
        }
        else Debug.Log("Field Scene is not exist....");

        switch (currntType)
        {
            case Type.Stage1:
            case Type.Stage2:
            case Type.Stage3:
                if (ui == null)
                {
                    Debug.Log("LoadLevelAdditive");
                    Application.LoadLevelAdditive(stageuiSceneName);    // UIが見つからなかったら追加
                    return false;
                }
                break;
            default: break;
        }
        return true;
    }

    // タイトルをロード
    void OnStartTitle()
    {
        loaded = false;
        currntType = Type.Title;
        // インターミッション開始
        BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideIn);
    }

    // 次のステージをロード
    void OnNextStage( Type setType=Type.None )
    {
        loaded = false;
        if (setType == Type.None)
        {
            int current = (int)currntType;
            current++;
            // ステージ数をオーバーしていた場合はTitleへ戻す
            if (current >= mainSceneName.Length) currntType = Type.Title;
            else currntType = (Type)(current);
        }
        else currntType = setType;
        // インターミッション開始
        BroadcastMessage("OnIntermissionStart", IntermissionEffector.Type.SlideIn);
    }

    // インターミッションの終了受け取り
    void OnIntermissionEnd()
    {
        if (loaded) logic.SendMessage("OnGameStart");   // ロード済みならゲームスタート
        else Load();        // ロードできてないならロード開始
    }

    // データ保存
    void OnSaveScore( int saveScore )
    {
        score = saveScore;
    }
}
