using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ƒqƒbƒg‚Éƒ‚ƒfƒ‹‚ğÁ‚·‚¾‚¯
/// </summary>
public class TorpedoModel : MonoBehaviour {


    void OnHit()
    {
        renderer.enabled = false;
    }
}
