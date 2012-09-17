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
    private string[] fieldSceneName = new string[] { 
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
        if( playOnAwake ) SendMessage("OnStartTitle");
    }

    void Load()
    {
        int index = (int)currntType;
        switch( currntType ) {
            case Type.Stage1:
            case Type.Stage2:
            case Type.Stage3:
                Application.LoadLevel(fieldSceneName[index]);       // フィールドシーンロード
                if (ui == null) {
                    Application.LoadLevelAdditive(stageuiSceneName);    // UIが見つからなかったら追加
                }
                break;
            case Type.Title:
                Application.LoadLevel(fieldSceneName[index]);
                break;

            case Type.None:
            default:
                Debug.Log("Field Scene is not exist....");
                break;
        }
        // 参照を保存
        InitReference();
        // インターミッション
        BroadcastMessage("StartIntermission");
    }

    void InitReference()
    {
        field = GameObject.Find("/Field");
        ui = GameObject.Find("/UI");

        if (field) loaded = true;
        else Debug.Log("Field Scene is not exist....");
    }

    // タイトルをロード
    void OnStartTitle()
    {
        loaded = false;
        currntType = Type.Title;
        BroadcastMessage("StartIntermission", IntermissionEffector.Type.SlideOut, SendMessageOptions.DontRequireReceiver);
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
            if (current >= fieldSceneName.Length) currntType = Type.Title;
            else currntType = (Type)(current);
        }
        else currntType = setType;
        // インターミッション
        BroadcastMessage("StartIntermission", IntermissionEffector.Type.SlideIn, SendMessageOptions.DontRequireReceiver);
    }

    // インターミッションの終了受け取り
    void OnIntermissionEnd()
    {
        if (loaded) field.SendMessage("OnGameStart");   // ロード済みならゲームスタート
        else Load();        // ロードできてないならロード開始
    }

    // データ保存
    void OnSaveScore( int saveScore )
    {
        score = saveScore;
    }
}
