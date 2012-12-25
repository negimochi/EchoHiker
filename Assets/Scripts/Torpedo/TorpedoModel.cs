using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ヒット時にモデルを消すだけ
/// </summary>
public class TorpedoModel : MonoBehaviour {


    void OnHit()
    {
        renderer.enabled = false;
    }
}
