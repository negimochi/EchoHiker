using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private int interval;
    [SerializeField]
    private int counter;
    [SerializeField]
    private bool state;

    private float param;

    void OnClick()
    {
        state = !state;
    }
    void OnClock( int delay )
    { 
        if( ++counter >= interval )  {
            if (state) {
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play((ulong)delay);
                param = 1.0f;
            }
            counter = 0;
        }
    }

    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        param *= Mathf.Exp(-5.0f * Time.deltaTime);
        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
        Color color = new Color(1.0f, 1.0f - param, state?0.0f:1.0f );
        renderer.material.color = color;

        if (state) {
            transform.localRotation *=
                Quaternion.AngleAxis(Time.deltaTime * 90.0f, Vector3.up) *
                Quaternion.AngleAxis(Time.deltaTime * 10.0f, Vector3.right);
        }
        else {
            transform.localRotation = Quaternion.identity;
        }
	}

}
