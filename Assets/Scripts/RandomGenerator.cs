using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject target;  // �����Ώ�
    [SerializeField]
    private Rect posRange;     // �����͈�(x,z)��Rect�w��
    [SerializeField]
    private float posY;         // ��������Y���W�ʒu
    [SerializeField]
    private bool fill;      // true: posXZ����S���ΏۂƂ��� 
                            // flase: posXZ�̊O�����ΏۂƂ���

    [SerializeField]
    private GenerateParameter param = new GenerateParameter();
//    [SerializeField]
//    private int limitNum = 1;  // �ő吔
//    [SerializeField]
//    private float delayTime = 1.0f;
//    [SerializeField]
//    private bool endless = true;   // ���~�b�g�����猸�������Ɏ����ǉ����邩

    private bool limitChecker;
    private bool ready;
    private GameObject[] childrenArray = null;
    private GameObject[] sonarArray = null;

    void Start()
    {
        childrenArray = GameObject.FindGameObjectsWithTag(target.tag);
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
        // 1�x���~�b�g�ɓ��B���Ă��āA�G���h���X�t���O�������Ă��Ȃ��Ƃ��͒ǉ����Ȃ�
        if (limitChecker && !param.endless) return false;
        // �����ł��ĂȂ�
        if (!ready) return false;
        // ���`�F�b�N
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
    /// [ Message ] �I�u�W�F�N�g�j��
    /// </summary>
    void OnDestroyObject()
    {
        // �z��X�V
        UpdateArray();
    }

    public void Generate()
    {
        Vector3 pos = new Vector3(posRange.xMin, posY, posRange.yMin);
        if (fill)
        {
            // posRange���Ƀ����_���Ɉʒu�����߂�
            pos.x += posRange.width * Random.value;
            pos.z += posRange.height * Random.value;
        }
        else {
            // posRange�O����Ƀ����_���Ɉʒu�����߂�
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

        // ����
        GameObject newObj = Object.Instantiate(target, pos, Quaternion.identity) as GameObject;
        // ������e�ɂ���
        newObj.transform.parent = transform;
        Debug.Log("generated[" + ChildrenNum() + "]=" + newObj.name);

        // �z��X�V
        UpdateArray();
    }

    private void UpdateArray()
    {
        childrenArray = GameObject.FindGameObjectsWithTag(target.tag);
        // �ʒm
        SendMessage("OnUpdateArray", childrenArray, SendMessageOptions.DontRequireReceiver);
    }

    public int ChildrenNum()
    {
        if (childrenArray != null) return childrenArray.Length;
        return 0;
    }

    // �Ǘ����Ă���q�̎Q��
    public GameObject[] ChildrenArray() { return childrenArray; }

    public void SetParam(GenerateParameter param_)
    {
        param = param_;
    }

}
