using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private float interval;
    [SerializeField]
    private float offset;
    private float counter;
    [SerializeField]
    private bool visible = true;
    [SerializeField]
    private bool valid = true;

    private float param;

    void OnClick()
    {
        valid = !valid;
    }
    void OnClock( int delay )
    {
        if (valid)
        {
            counter += Time.deltaTime;
            if (counter >= interval)
            {
               // AudioSource audio = GetComponent<AudioSource>();
                audio.Play((ulong)delay);
                param = 1.0f;
                counter = 0;
                Debug.Log(name + ":Play");
            }
        }
    }

    // Use this for initialization
	void Start () 
    {
        counter = offset;
        renderer.enabled = visible;
        param = 1.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        OnClock(0);
        if (visible) {
            param *= Mathf.Exp(-5.0f * Time.deltaTime);
//	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
            Color color = new Color(1.0f, 1.0f - param, valid ? 0.0f : 1.0f);
	        renderer.material.color = color;
			
	        if (valid) {
//	            transform.localRotation *=
//	                Quaternion.AngleAxis(Time.deltaTime * 90.0f, Vector3.up) *
//	                Quaternion.AngleAxis(Time.deltaTime * 20.0f, Vector3.right);
	        }
	        else {
	            transform.localRotation = Quaternion.identity;
	        }
		}
	}

}
