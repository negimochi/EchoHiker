using UnityEngine;
using System.Collections;

/// <summary>
/// �q�b�g���Ƀp�[�e�B�N������������
/// </summary>
public class TorpedoMoveEffect : MonoBehaviour {

    void OnHit()
    {
        particleSystem.Stop();
    }
}
