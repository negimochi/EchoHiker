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
    [SerializeField]
    private float startAngle;
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

    public void Generate()
    {
        //if (target == null) return;

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
        float baseAngle = Vector3.Angle(pos, Vector3.zero);
        Debug.Log("baseAngle = " + baseAngle);
        float halfAngle = startAngle * 0.5f;
        ///float rotY = Random.Range(baseAngle-halfAngle, baseAngle+halfAngle);
        float rotY = baseAngle;
        Quaternion rot = Quaternion.Euler(new Vector3(0.0f, rotY, 0.0f));

        // 生成
        GameObject newObj = Object.Instantiate(target, pos, rot) as GameObject;
        newObj.transform.parent = transform;
        Debug.Log("generated item[" + size + "]=" + newObj.name);
        // カウンタ
        size += 1;
    }

    public bool IsSizeOver()
    {
        return (limitSize <= size)?true:false;
    }

    private IEnumerable Delay(float time)
    {
        Debug.Log("RandomGenerator.Auto");
        yield return new WaitForSeconds(time);
        Generate();
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
            //Debug.Log("RandomGenerator.Update");
            //StartCoroutine("Delay", delaytime);
        }
    }
}
