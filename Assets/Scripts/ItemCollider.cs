using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour
{
    [SerializeField]
    public float waitTime = 5.0f;
    [SerializeField]
    private int scoreMax = 1000;
    [SerializeField]
    public int scoreMin = 50;
    [SerializeField]
    public int step = 10;

    private int scoreValue = 100;
    private bool isFinished;

    void Start()
    {
        isFinished = false;
        collider.isTrigger = true;  // �g���K�[�����ĂĂ���

        // �����Ŋl���|�C���g���U�炷
        scoreValue = scoreMax;
        StartCoroutine("ChangeScoreValue");
    }

    private IEnumerator ChangeScoreValue()
    {
        yield return new WaitForSeconds(waitTime);

        scoreValue -= step;
        if(scoreValue < scoreMin) {
           scoreValue = scoreMin;
        }
        else {
            StartCoroutine("ChangeScoreValue");
        }
    }

    void OnDestroyObject()
    {
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject.name, SendMessageOptions.DontRequireReceiver);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isFinished) return; // 1�񂾂��Փ˂��݂����̂ł��̊Ď��p�B
                                // isTrigge=false���Ă�������Ƃ��Ă��܂��B

        if (collider.CompareTag("Player"))   // �v���C���[������
        {
            isFinished = true;
            // HitItem�ʒm
            //obj.SendMessage("OnHitItem");
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnGetItem", scoreValue, SendMessageOptions.DontRequireReceiver);

            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
