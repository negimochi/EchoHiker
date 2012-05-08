using UnityEngine;
using System.Collections;

public class PreciseClock : MonoBehaviour {
    private static int kLowestFPS = 20;
    private static float kBpm = 120.0f;
    private float nextClock;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time + 1.0 / kLowestFPS > nextClock) { 
            AudioSource audio = GetComponent<AudioSource>();
            int delay = audio.clip.samples - audio.timeSamples;
            gameObject.BroadcastMessage("OnClock", delay);
            nextClock += 60.0f / kBpm / 4.0f;
        }
	}
}
