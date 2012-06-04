using UnityEngine;
using System.Collections;

/// <summary>
/// �A�C�e���̐ڐG����
/// </summary>
public class ItemCollider : MonoBehaviour
{

    // [SerializeField]
    // private int type;

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
        GameObject obj = collider.gameObject;
        if (obj.tag.Equals("Player"))   // �v���C���[������
        {
            isFinished = true;
            // HitItem�ʒm
            //obj.SendMessage("OnHitItem");
            GameObject ui = GameObject.Find("/UI");
            if (ui) ui.SendMessage("OnHitItem");
            Note note = GetComponent<Note>();
            if (note) note.SendMessage("OnHitItem");
        }
    }
}
