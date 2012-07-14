using UnityEngine;
using System.Collections;

public class TorpedoMoveEffect : MonoBehaviour {

    void OnHit()
    {
        particleSystem.Stop();
    }
}
