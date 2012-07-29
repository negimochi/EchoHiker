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

//    private GameObject[] childrenArray = null;
//    private GameObject[] sonarArray = null;
    private ArrayList childrenArray = new ArrayList();
    private ArrayList sonarArray = new ArrayList();

//    private GameObject sonarCameraObj = null;
   
    void Start()
    {
//        sonarCameraObj = GameObject.Find("/Player/SonarCamera");

        // �����z�u��������ꍇ�͂����œo�^���Ă���
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
    void OnDestroyObject( GameObject target )
    {
        //UpdateArray();
        // �z��Ɏc���Ă���΍폜
        childrenArray.Remove(target);
        sonarArray.Remove(target);
        // �q�����������ʒm
        SendMessage("OnDestroyChild", target, SendMessageOptions.DontRequireReceiver);

        Destroy(target);
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

        // �C���X�^���X����
        GameObject newChild = Object.Instantiate(target, pos, Quaternion.identity) as GameObject;
        // ������e�ɂ���
        newChild.transform.parent = transform;
        Debug.Log("generated[" + ChildrenNum() + "]=" + newChild.name);

        // �z��X�V
        //UpdateArray();
        childrenArray.Add(newChild);
        sonarArray.Add(newChild);
        // �q���𑝂₵���ʒm
        SendMessage("OnInstantiatedChild", newChild, SendMessageOptions.DontRequireReceiver);
        // �\�i�[�J�����ɂ��`����
        //sonarCameraObj.SendMessage("OnInstantiatedChild", newChild);
    }

    /*
    // �蔲�����W
    private void UpdateArray()
    {
        childrenArray = GameObject.FindGameObjectsWithTag(target.tag);
        // OnUpdateArray������Βʒm
        SendMessage("OnUpdateArray", childrenArray, SendMessageOptions.DontRequireReceiver);
    }
    */

    public int ChildrenNum()
    {
        if (childrenArray != null) return childrenArray.Count;
        return 0;
    }

    // �Ǘ����Ă���q�̎Q��
    public ArrayList Children() { return childrenArray; }
    // �\�i�[�ɂ������������Ƃ��Ă���
    public ArrayList SonarChildren() { return sonarArray; }
    // �����p�����[�^�Z�b�g
    public void SetParam(GenerateParameter param_) {  param = param_; }

}
