using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �q�b�g���Ƀ��f������������
/// </summary>
public class TorpedoModel : MonoBehaviour {


    void OnHit()
    {
        renderer.enabled = false;
    }
}
