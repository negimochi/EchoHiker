using UnityEngine;
using System.Collections;

public class AirgageMeter : MonoBehaviour {

    /// <summary>
    /// [SendMessage]�l�X�V
    /// </summary>
    /// <param name="value">�X�V�l[0,1]</param>
    void OnDisplay(float value)
    {
        // �V�F�[�_�̃A���t�@cutoff�̒l��ύX���ĕ\���X�V
        Debug.Log("OnDeflate: Air=" + value);
        renderer.material.SetFloat("_Cutoff", value);
    }
}
