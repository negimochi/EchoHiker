using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;  // 生成対象
    [SerializeField]
    private float posY;         // 生成するY座標位置

    [SerializeField]
    private GenerateParameter param = new GenerateParameter();

    [SerializeField]
    private int counter = 0;
    [SerializeField]
    private bool limitCheck = false;
    [SerializeField]
    private bool ready = false;

    private ArrayList childrenArray = new ArrayList();
    private ArrayList sonarArray = new ArrayList();
   
    void Start()
    {
        // 初期配置分がある場合はここで登録しておく
        GameObject[] children = GameObject.FindGameObjectsWithTag(target.tag);
        for (int i = 0; i < children.Length; i++ )
        {
            childrenArray.Add(children[i]);
            sonarArray.Add(children[i]);
        }
    }

//    void Update()
//    {
//        if (TimingCheck())
//        {   
//            Generate();
//            ready = false;
//            StartCoroutine("Delay");
//        }
//    }

    private bool TimingCheck()
    {
        // 準備できてない
        if (!ready) return false;
        // 1度リミットに到達していて、エンドレスフラグが立っていないときは追加しない
        if (!param.endless && limitCheck) return false;

        Debug.Log("num check");

        // 個数チェック
        return (ChildrenNum() < param.limitNum) ? true : false;
    }

//    private IEnumerator Delay()
//    {
//        yield return new WaitForSeconds(param.delayTime);
//
//        ready = true;
//
//        if (TimingCheck())
//        {   
//            Generate();
//            ready = false;
//            StartCoroutine("Delay");
//        }
//
//    }

    void OnStart()
    {
        counter = 0;
        ready = true;
        limitCheck = false;

        Generate();
    }

    void OnSuspend()
    {
        ready = false;
        //StopCoroutine("Delay");
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

        counter++;
        if (counter > param.limitNum)
        {
            limitCheck = true;
            Debug.Log("/// LIMIT ////");
        }
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
