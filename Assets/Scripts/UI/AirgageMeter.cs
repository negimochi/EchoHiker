using UnityEngine;
using System.Collections;

public class AirgageMeter : MonoBehaviour {

    /// <summary>
    /// [SendMessage]値更新
    /// </summary>
    /// <param name="value">更新値[0,1]</param>
    void OnDisplay(float value)
    {
        // シェーダのアルファcutoffの値を変更して表示更新
        Debug.Log("OnDeflate: Air=" + value);
        renderer.material.SetFloat("_Cutoff", value);
    }
}
