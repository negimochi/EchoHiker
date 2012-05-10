using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private int interval;
    [SerializeField]
    private int counter;
    [SerializeField]
    private bool valid;
    [SerializeField]
    private bool visible = true;

    private float param;

    void OnClick()
    {
        valid = !valid;
    }
    void OnClock( int delay )
    { 
        if( ++counter >= interval )  {
            if (valid) {
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play((ulong)delay);
                param = 1.0f;
            }
            counter = 0;
        }
    }

    // Use this for initialization
	void Start () {
        renderer.enabled = visible;
    }
	
	// Update is called once per frame
	void Update () {
        param *= Mathf.Exp(-5.0f * Time.deltaTime);
		
		if( visible ) {
	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
            Color color = new Color(1.0f, 1.0f - param, valid ? 0.0f : 1.0f);
	        renderer.material.color = color;
			
	        if (valid) {
	            transform.localRotation *=
	                Quaternion.AngleAxis(Time.deltaTime * 90.0f, Vector3.up) *
	                Quaternion.AngleAxis(Time.deltaTime * 10.0f, Vector3.right);
	        }
	        else {
	            transform.localRotation = Quaternion.identity;
	        }
		}
	}

}
