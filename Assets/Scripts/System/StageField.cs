using UnityEngine;
using System.Collections;

public class StageField : MonoBehaviour {


    // ロード終了時 LoadLevelAdditiveはOnLevelWasLoadedを呼ばないのでAwakeで処理
//    void OnLevelWasLoaded(int level)
    void Start()
    { 
    }
    void Awake()
    {
        Debug.Log("Stage Loaded");
        // ロード完了の通知
        GameObject adapter = GameObject.Find("/Adapter");
        if (adapter) adapter.SendMessage("OnLoadedField");
        else Debug.Log("Adapter is not exist!!!");
    }
}
