using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;  // 生成対象
//    [SerializeField]
//    private Rect posRange;     // 生成範囲(x,z)をRect指定
    [SerializeField]
    private float posY;         // 生成するY座標位置
//    [SerializeField]
//    private bool fill;     // true: posXZ内を全部対象とする 
                            // flase: posXZの外周上を対象とする

    [SerializeField]
    private GenerateParameter param = new GenerateParameter();
//    [SerializeField]
//    private int limitNum = 1;  // 最大数
//    [SerializeField]
//    private float delayTime = 1.0f;
//    [SerializeField]
//    private bool endless = true;   // リミット数から減った時に自動追加するか

    private bool limitChecker;
    private bool ready;

//    private GameObject[] childrenArray = null;
//    private GameObject[] sonarArray = null;
    private ArrayList childrenArray = new ArrayList();
    private ArrayList sonarArray = new ArrayList();

//    private GameObject sonarCameraObj = null;
   
    void Start()
    {
//        sonarCameraObj = GameObject.Find("/Player/SonarCamera");

        // 初期配置分がある場合はここで登録しておく
        GameObject[] children = GameObject.FindGameObjectsWithTag(target.tag);
        for (int i = 0; i < children.Length; i++ )
        {
            childrenArray.Add(children[i]);
            sonarArray.Add(children[i]);
        }
        ready = false;
        limitChecker = false;
    }

    void Update()
    {
        if (TimingCheck())
        {
            Generate();
            ready = false;
            StartCoroutine("Delay");
        }
    }

    public bool TimingCheck()
    {
        // 1度リミットに到達していて、エンドレスフラグが立っていないときは追加しない
        if (limitChecker && !param.endless) return false;
        // 準備できてない
        if (!ready) return false;
        // 個数チェック
        return (ChildrenNum() < param.limitNum) ? true : false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(param.delayTime);
        ready = true;
    }

    void OnGameStart()
    {
        ready = true;
    }

    void OnGameOver()
    {
        ready = false;
        StopCoroutine("Delay");
    }

    /// <summary>
    /// [ Message ] オブジェクト破壊
    /// </summary>
    void OnDestroyObject( GameObject target )
    {
        //UpdateArray();
        // 配列に残っていれば削除
        childrenArray.Remove(target);
        sonarArray.Remove(target);
        // 子供が減った通知
        SendMessage("OnDestroyChild", target, SendMessageOptions.DontRequireReceiver);

        Destroy(target);
    }

    public void Generate()
    {
        Rect posRange = param.pos;
        Vector3 pos = new Vector3(posRange.xMin, posY, posRange.yMin);
        if (param.fill)
        {
            // posRange内にランダムに位置を決める
            pos.x += posRange.width * Random.value;
            pos.z += posRange.height * Random.value;
        }
        else {
            // posRange外周上にランダムに位置を決める
            if (Random.Range(0, 2) == 1)
            {
                pos.x += posRange.width * Random.value;
                if (Random.Range(0, 2) == 1) pos.z = posRange.yMax;
            }
            else
            {
                if (Random.Range(0, 2) == 1) pos.x = posRange.xMax;
                pos.z += posRange.height * Random.value;
            }
        }

        // インスタンス生成
        GameObject newChild = Object.Instantiate(target, pos, Quaternion.identity) as GameObject;
        // 自分を親にする
        newChild.transform.parent = transform;
        Debug.Log("generated[" + ChildrenNum() + "]=" + newChild.name);

        // 配列更新
        //UpdateArray();
        childrenArray.Add(newChild);
        sonarArray.Add(newChild);
        // 子供を増やした通知
        SendMessage("OnInstantiatedChild", newChild, SendMessageOptions.DontRequireReceiver);
        // ソナーカメラにも伝える
        //sonarCameraObj.SendMessage("OnInstantiatedChild", newChild);
    }

    /*
    // 手抜き収集
    private void UpdateArray()
    {
        childrenArray = GameObject.FindGameObjectsWithTag(target.tag);
        // OnUpdateArrayがあれば通知
        SendMessage("OnUpdateArray", childrenArray, SendMessageOptions.DontRequireReceiver);
    }
    */

    public int ChildrenNum()
    {
        if (childrenArray != null) return childrenArray.Count;
        return 0;
    }

    // 管理している子の参照
    public ArrayList Children() { return childrenArray; }
    // ソナーにあたった分をとっておく
    public ArrayList SonarChildren() { return sonarArray; }
    // 生成パラメータセット
    public void SetParam(GenerateParameter param_) {  param = param_; }

}
