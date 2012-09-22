using UnityEngine;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

    enum Type
    {
        None = 0,
        OnlyOne,
        Random,
        All,
    };
    [SerializeField]
    private Type type = Type.None;
    [SerializeField]
    private TargetObjct current = null;

    public class TargetObjct
    {
        public string tag;
        public bool clear = false;
        public RandomGenerator generater = null;
    };

    private ArrayList targetArr = new ArrayList();

    private bool valid = true;


	void Start () 
    {
        GameObject enemyObj = GameObject.Find("Enemies");
        GameObject itemObj = GameObject.Find("Items");
        if (enemyObj) AddGenerator( "Enemy", enemyObj.GetComponent<RandomGenerator>() );
        if (itemObj) AddGenerator("Item", itemObj.GetComponent<RandomGenerator>());
    }

    private void AddGenerator( string tag, RandomGenerator generater )
    {
        if (generater == null) return;

        TargetObjct targetObj = new TargetObjct();
        targetObj.tag = tag;
        targetObj.generater = generater;
        targetArr.Add(targetObj);
    }
	
	void Update () 
    {
        if (!valid) return;

        {
            switch( type ) {
                default:
                case Type.None:     break;
                case Type.OnlyOne: Generate_OnlyOne(); break;
                case Type.Random: Generate_Random(); break;
                case Type.All: Generate_All(); break;
            }
        }
    }

    private bool Check()
    {
        return false;
    }

    private void Generate_OnlyOne()
    {
        if (current!=null) current.generater.Generate();
    }

    private void Generate_Random()
    {
    }

    private void Generate_All()
    {
        ;
    }

    /*
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
    */

    void OnGameStart()
    {
        // ゲーム開始時
        valid = true;
    }

    void OnGameOver()
    {
        // ゲームオーバー時
        valid = false;
    }

    void OnClearCondition(string tag)
    {
        bool allclear = true;
        foreach( TargetObjct targetObj in targetArr ) 
        {
            // 条件を達成していたタグのTargetObjectをtrueにする
            if (tag.CompareTo(targetObj.tag) == 0) targetObj.clear = true;

            // 全部クリアできてるかチェック
            allclear |= targetObj.clear;
        }

        if (allclear) {
            // ゲーム終了、次のステージへ
            GameObject adapter = GameObject.Find("/Adapter");
            adapter.SendMessage("OnGameEnd", true);
            valid = false;
        }
    }
}
