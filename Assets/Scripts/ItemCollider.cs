using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour
{
    // �X�R�A
    [SerializeField]
    public int scoreValue = 100;
    [SerializeField]
    private int lifetime = 1000;

    private bool isFinished;

    void Start()
    {
        isFinished = false;
        collider.isTrigger = true;  // �g���K�[�����ĂĂ���
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
            if (ui) ui.SendMessage("OnHitItem", gameObject);

            Note note = GetComponent<Note>();
            if (note) note.SendMessage("OnHit");
        }
    }
}
