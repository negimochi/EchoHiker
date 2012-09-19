using UnityEngine;
using System.Collections;

public class StageUI : MonoBehaviour {


	void Awake()
	{
		// OnLoadでDestory対象からはずす
        DontDestroyOnLoad(gameObject);
	}

    // スコアを返す
    public int Score()
    {
        GameObject scoreDisp = GameObject.Find("ScoreDisplay");
        if (scoreDisp) return scoreDisp.GetComponent<ScoreDisplay>().Score();
        return 0;
    }
}
