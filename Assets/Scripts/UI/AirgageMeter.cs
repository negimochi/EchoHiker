using UnityEngine;
using System.Collections;

public class AirgageMeter : MonoBehaviour {

    /// <summary>
    /// [SendMessage]�l�X�V
    /// </summary>
    /// <param name="value">�X�V�l[0,1]</param>
    void OnDisplayAirgage(float value)
    {
        //Debug.Log("OnDeflate: Air=" + value);
        // �V�F�[�_�̃A���t�@cutoff�̒l��ύX���ĕ\���X�V
        renderer.material.SetFloat("_Cutoff", value);
    }
}
