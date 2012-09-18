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
    private Type currntType = Type.None;
    [SerializeField]
    private bool playOnAwake = true;

    private GameObject logic = null;
//    private GameObject field = null;
//    private GameObject ui = null;

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

    bool Load()
    {
        if(currntType == Type.None) return false;
        int index = (int)currntType;
        Application.LoadLevel(mainSceneName[index]);       // フィールドシーンロード
        return true;
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
        logic = GameObject.Find("/Logic");
//        field = GameObject.Find("/Field");
//        ui = GameObject.Find("/UI");
        if (logic)
        {
            // logicさえあればロードできたとみなす
            loaded = true;
            return true;
        }

        Debug.Log("Logic is not exist....");
        return false;
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
