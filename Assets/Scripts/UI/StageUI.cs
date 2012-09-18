using UnityEngine;
using System.Collections;

public class StageUI : MonoBehaviour {


	void Awake()
	{
		// OnLoadでDestory対象からはずす
        DontDestroyOnLoad(gameObject);
	}

}
