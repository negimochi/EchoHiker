using UnityEngine;
using System.Collections;

public class StageField : MonoBehaviour {


    // ロード終了時 LoadLevelAdditiveはOnLevelWasLoadedを呼ばないのでAwakeで処理
//    void OnLevelWasLoaded(int level)
    void Awake()
    {
        Debug.Log("Stage Loaded");
        GameObject adapter = GameObject.Find("/Adapter");
        if (adapter) adapter.SendMessage("OnLoadedField");
        else Debug.Log("Adapter is not exist!!!");
    }
}
