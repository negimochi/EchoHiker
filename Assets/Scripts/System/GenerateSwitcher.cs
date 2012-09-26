using UnityEngine;
using System.Collections;

public class GenerateSwitcher : MonoBehaviour {

    enum Type
    {
        None = 0,
        OnlyOne,
        Switch,
//        Random,
//        All,
    };
    [SerializeField]
    private Type type = Type.None;
    [SerializeField]
    private string currentTag = "Enemey";

    public class TargetGenerator
    {
        public bool clear = false;
        public RandomGenerator gen = null;
    };

    private TargetGenerator current = null;
    private Hashtable generators = new Hashtable();

    private bool valid = true;

	void Start () 
    {
    }

    private void Init()
    {
        GameObject enemyObj = GameObject.Find("Enemies");
        GameObject itemObj = GameObject.Find("Items");
        if (enemyObj) AddGenerator( enemyObj.GetComponent<RandomGenerator>() );
        if (itemObj) AddGenerator( itemObj.GetComponent<RandomGenerator>() );
    }

    private void AddGenerator(  RandomGenerator generater )
    {
        Debug.Log("AddGenerator");
        if (generater == null) return;

        GameObject target = generater.Target();
        Debug.Log("Target:" + target.tag);
        if (target == null) return;
        string tag = target.tag;

        TargetGenerator targetGenerator = new TargetGenerator();
        targetGenerator.clear = false;
        targetGenerator.gen = generater;
        generators.Add(tag, targetGenerator);
    }
	

    void OnSwitchCheck( string key )
    {
        if (currentTag.CompareTo(key) == 0)
        {
            if (type == Type.Switch) Switch();
        }
    }

    private void Run()
    {
        if (!generators.ContainsKey(currentTag))
        {
            Debug.Log(currentTag + ": Not Exist!");
            return;
        }
        current = generators[currentTag] as TargetGenerator;
        current.gen.SendMessage("OnGeneratorStart");
    }

    private void Switch()
    {
        if (generators.Count == 0) return;

        current.gen.SendMessage("OnGeneratorSuspend");

        foreach( string key in generators.Keys )
        {
            if (currentTag.CompareTo(key) != 0) {
                currentTag = key;
                Run();
                return;
            }
        }
    }

    void OnGameStart()
    {
        // ゲーム開始時
        Init();
        valid = true;
        Run();
    }

    void OnGameOver()
    {
        // ゲームオーバー時
        valid = false;
    }


    // クリア条件
    void OnClearCondition(string tag)
    {
        bool allclear = true;
        foreach (string key in generators.Keys) 
        {
            // 条件を達成していたタグのTargetObjectをtrueにする

            TargetGenerator target = generators[key] as TargetGenerator;
            if (tag.CompareTo(key) == 0) target.clear = true;
            // 全部クリアできてるかチェック
            allclear &= target.clear;
        }

        if (allclear) {
            // ゲーム終了、次のステージへ
            GameObject adapter = GameObject.Find("/Adapter");
            adapter.SendMessage("OnGameEnd", true);
            valid = false;
        }
    }
}
