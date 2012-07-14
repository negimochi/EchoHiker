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
    private int limitNum;  // �ő吔
    [SerializeField]
    private float delayTime = 1.0f;
    [SerializeField]
    private bool endless = true;   // ���~�b�g�����猸�������Ɏ����ǉ����邩

    private bool limitChecker;
    private bool ready;
    private float counter;
    private GameObject[] children = null;

    void Start()
    {
        ready = true;
        limitChecker = false;
        counter = 0.0f;
    }

    void Update()
    {
        if (TimingCheck())
        {
            Generate();
            ready = false;
            StartCoroutine("Delay");
//            counter += Time.deltaTime;
//            if (counter >= delaytime)
//            {
//                counter = 0.0f;
//            }
        }
    }

    public bool TimingCheck()
    {
        // 1�x���~�b�g�ɓ��B���Ă��āA�G���h���X�t���O�������Ă��Ȃ��Ƃ��͒ǉ����Ȃ�
        if (limitChecker && !endless) return false;
        // �����ł��ĂȂ�
        if (!ready) return false;
        // ���`�F�b�N
        return (ChildrenNum() < limitNum)?true:false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        ready = true;
    }

    /// <summary>
    /// [ Message ] �I�u�W�F�N�g�j��
    /// </summary>
    /// <param name="obj"></param>
    void OnDestroyObject(GameObject obj)
    {
        Debug.Log("Destoryed:" + obj.name);
        // �z��X�V
        children = GameObject.FindGameObjectsWithTag(target.tag);
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

        // �������������Ă���
        if( target.CompareTag("Enemy") ) {
            newObj.transform.LookAt(Vector3.zero);
        }
        // �z��X�V
        children = GameObject.FindGameObjectsWithTag(target.tag);

        // �ʒm
        //SendMessage("OnGenerated", SendMessageOptions.DontRequireReceiver);
    }

    public int ChildrenNum()
    {
        if (children != null) return children.Length;
        return 0;
    }

}
