using UnityEngine;
using System.Collections;

public class TorpedoMoveEffect : MonoBehaviour {

    void OnHit()
    {
//        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }
}
