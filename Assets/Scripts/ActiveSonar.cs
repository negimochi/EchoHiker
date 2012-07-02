using UnityEngine;
using System.Collections;

public class ActiveSonar : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float stepStart = 1.0f;
    [SerializeField]
    private float stepEnd =300.0f;

    [SerializeField]
    private float updateTime = 0.2f;

    private float currentTime = 0.0f;
    private bool search = false;
    private CapsuleCollider capsuleCollider = null;

	void Start () 
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (search) {
            float time = currentTime / duration;
            if (time <= 1.0f)
            {
                float step = Mathf.SmoothStep(stepStart, stepEnd, time);
                capsuleCollider.radius = step;

                // ŽžŠÔXV
                currentTime += Time.deltaTime;
            }
            else
            {
                StartCoroutine("Delay", delay);
            }
        }
	}

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentTime = 0.0f;
    }


    void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit Sonar:" + other.gameObject.tag + ", " + Time.time);
        if (other.gameObject.tag.Equals("Enemy") ||
            other.gameObject.tag.Equals("Item") ||
            other.gameObject.tag.Equals("Torpedo"))
        {
            other.gameObject.BroadcastMessage("OnSonarHit", SendMessageOptions.DontRequireReceiver);
        }

    }


    public void Search()
    {
        Debug.Log("ActiveSoner.Search");
        search = true;
        currentTime = 0.0f;
        capsuleCollider.radius = 1.0f;
    }

    public void Reset()
    {
        Debug.Log("ActiveSoner.Reset");
        capsuleCollider.radius = 1.0f;
        search = false;
    }
}
