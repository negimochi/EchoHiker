using UnityEngine;
using System.Collections;

public class TorpedoMoveEffect : MonoBehaviour {

    void OnHit()
    {
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();
        particleSystem.enableEmission = false;
    }
}
