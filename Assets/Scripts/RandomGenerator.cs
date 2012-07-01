using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Rect startLine;
    [SerializeField]
    private float posY;
    [SerializeField]
    private bool fill;
//    [SerializeField]
//    private float startAngle = 30.0f;
    [SerializeField]
    private int limitSize;
    [SerializeField]
    private float delaytime = 1.0f;

    private float counter;

    private int size;
    public int Size {
        get { return size; }
    }

    void Start()
    {
        size = 0;
        counter = 0.0f;
    }

    private void OnDestroyObject( string name )
    {
        Debug.Log("Destoryed:" + name);
        size--;
        if (size < 0)
        {
            Debug.LogError("Size < 0");
            size = 0;
        }
    }

    public void Generate()
    {
        // 外周上にランダムに位置を決める
        Vector3 pos = new Vector3( startLine.xMin, posY, startLine.yMin);
        if (fill)
        {
            pos.x += startLine.width * Random.value;
            pos.z += startLine.height * Random.value;
        }
        else {
            if (Random.Range(0, 2) == 1)
            {
                pos.x += startLine.width * Random.value;
                if (Random.Range(0, 2) == 1) pos.z = startLine.yMax;
            }
            else
            {
                if (Random.Range(0, 2) == 1) pos.x = startLine.xMax;
                pos.z += startLine.height * Random.value;
            }
        }

        // 向きもある程度ランダムに決める
//        float halfAngle = startAngle * 0.5f;
//        Vector3 rotVec =  new Vector3( 0.0f, Random.Range(-halfAngle, halfAngle), 0.0f );
//        Quaternion deltaRot = Quaternion.Euler(rotVec * Time.deltaTime);
        // 生成
        GameObject newObj = Object.Instantiate(target, pos, Quaternion.identity) as GameObject;
        newObj.transform.LookAt(Vector3.zero);
        newObj.transform.parent = transform;
        Debug.Log("generated[" + size + "]=" + newObj.name);

        // カウンタ
        size += 1;

        // 通知
        SendMessage("OnGenerated", new DictionaryEntry(newObj, 0), 
                                    SendMessageOptions.DontRequireReceiver);
    }

    public bool IsSizeOver()
    {
        return (limitSize <= size)?true:false;
    }

    void Update()
    {
        if (!IsSizeOver())
        {
            counter += Time.deltaTime;
            if (counter >= delaytime)
            {
                Generate();
                counter = 0.0f;
            }
        }
    }
}
