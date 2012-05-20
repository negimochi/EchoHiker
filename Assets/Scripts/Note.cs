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
    private Color baseColor;

    IEnumerator OnHitItem()
    {
        valid = false;
//        gameObject.GetComponent<>
        audio.Stop();
        yield return new WaitForSeconds(1); 
        Destroy(gameObject);    // é©ï™ÇçÌèú
//        transform.parent.gameObject.GetComponent<>();
    }

    void OnClock( int delay )
    {
        if (valid)
        {
            counter ++;
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
        baseColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b);
        param = 1.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (valid)
        {
            OnClock(0);
            if (visible)
            {
                param *= Mathf.Exp(-3.0f * Time.deltaTime);
                //	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
                Color color = new Color(Mathf.Abs(baseColor.r - param), baseColor.g, baseColor.b);
                renderer.material.color = color;
            }
        }
	}

}
