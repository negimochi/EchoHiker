using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TorpedoModel : MonoBehaviour {


    void OnHit()
    {
        renderer.enabled = false;
    }
}
