using UnityEngine;
using System.Collections;

public class MarineSnow : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGameOver() {
        particleSystem.Pause();
    }
}
