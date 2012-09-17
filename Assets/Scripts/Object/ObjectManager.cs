using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {


    private ArrayList generators = null;
    private RandomGenerator currentGen = null;
    private int current = 0;

	void Start () 
    {
        // 全Generatorの配列
//      generators = gameObject.GetComponentsInChildren<RandomGenerator>();
        generators = new ArrayList();
        GameObject enemyObj = GameObject.Find("/Object/EnemyManager");
        GameObject itemObj = GameObject.Find("/Object/ItemManager");
        if (itemObj) generators.Add(itemObj.GetComponent<RandomGenerator>());
        if (enemyObj) generators.Add(enemyObj.GetComponent<RandomGenerator>());
    }
	
	void Update () 
    {
        if (currentGen == null) return;
        if (currentGen.ChildrenNum() == 0)
        {
            // オブジェクト数がゼロなら生成タイプを変更
            Switch();
            // 開始
            Run();
        }
	}

    private void Run()
    {
        if (currentGen == null) return;

        currentGen.SendMessage("OnStart");
    }

    private void Switch()
    {
        if (generators.Count == 0) return;

        currentGen.SendMessage("OnSuspend");

        current++;
        if (current >= generators.Count) current = 0;
        Debug.Log("current=" + current);
        currentGen = generators[current] as RandomGenerator;
    }

    void OnGameStart()
    {
        if( generators.Count == 0 ) {
            Debug.Log("RandomGenerator is not exists..");
            Application.Quit();
        }

//        foreach (RandomGenerator gen in generators)
//        {
//            gen.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
//        }

        // ゲームを始める
        current = 0;
        currentGen = generators[current] as RandomGenerator;
        Run();
    }

    void OnGameOver()
    {
        foreach( RandomGenerator gen in generators ) {
            gen.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        }
        // ゲームを終わる。
        currentGen = null;
    }
}
