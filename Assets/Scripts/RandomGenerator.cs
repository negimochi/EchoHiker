using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;  // 生成対象
    [SerializeField]
    private Rect posRange;     // 生成範囲(x,z)をRect指定
    [SerializeField]
    private float posY;         // 生成するY座標位置
    [SerializeField]
    private bool fill;      // true: posXZ内を全部対象とする 
                            // flase: posXZの外周上を対象とする
    [SerializeField]
    private int limitNum;  // 最大数
    [SerializeField]
    private float delayTime = 1.0f;
    [SerializeField]
    private bool endless = true;   // リミット数から減った時に自動追加するか

    private bool limitChecker;
    private bool ready;
    private GameObject[] childrenArray = null;
    private GameObject[] sonarArray = null;

    void Start()
    {
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
        if (limitChecker && !endless) return false;
        // 準備できてない
        if (!ready) return false;
        // 個数チェック
        return (ChildrenNum() < limitNum)?true:false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
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
    void OnDestroyObject()
    {
        // 配列更新
        UpdateArray();
    }

    public void Generate()
    {
        Vector3 pos = new Vector3(posRange.xMin, posY, posRange.yMin);
        if (fill)
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

        // 生成
        GameObject newObj = Object.Instantiate(target, pos, Quaternion.identity) as GameObject;
        // 自分を親にする
        newObj.transform.parent = transform;
        Debug.Log("generated[" + ChildrenNum() + "]=" + newObj.name);

        // 中央を向かせておく
        if( target.CompareTag("Enemy") ) {
            newObj.transform.LookAt(Vector3.zero);
        }

        // 配列更新
        UpdateArray();
    }

    private void UpdateArray()
    {
        // 配列更新
        childrenArray = GameObject.FindGameObjectsWithTag(target.tag);
        // 通知
        SendMessage("OnUpdateArray", childrenArray, SendMessageOptions.DontRequireReceiver);
    }

    public int ChildrenNum()
    {
        if (childrenArray != null) return childrenArray.Length;
        return 0;
    }

    public GameObject[] ChildrenArray() { return childrenArray; }

}
