using UnityEngine;
using System.Collections;

public class SonarCamera : MonoBehaviour {

    private float radius = 0.0f;

    void Start()
    {
        SphereCollider shereCollider = GetComponent<SphereCollider>();
        if (shereCollider)
        {
            radius = shereCollider.radius;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckObject(other.gameObject))
        {
            other.gameObject.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
        }
    }
    void OnTriggerStay(Collider other)
    {
        ;
    }
    void OnTriggerExit(Collider other)
    {
        if (CheckObject(other.gameObject))
        {
            other.gameObject.BroadcastMessage("OnSonarOutside", SendMessageOptions.DontRequireReceiver);
        }
    }

    private bool CheckObject( GameObject target )
    {
        return (target.CompareTag("Torpedo") ||
                 target.CompareTag("Item") ||
                 target.CompareTag("Enemy")) ? true : false;
    }

    public void Check( GameObject target )
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= radius)
        {
            target.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
        }
        else {
            target.BroadcastMessage("OnSonarOutside", SendMessageOptions.DontRequireReceiver);
        }
    }

    // ï\é¶à íuí≤êÆ
	public void SetRect( Rect rect )
    {
        //Camera sonarCamera = gameObject.GetComponent<Camera>();

        float r = rect.width * 0.5f;
        float newWidth = r * Mathf.Pow(2.0f,0.5f);   // ì‡ê⁄Ç∑ÇÈê≥ï˚å`ÇÃàÍï”
        float sub = (rect.width - newWidth)*0.5f;
        camera.pixelRect = new Rect(rect.x + sub, rect.y + sub, newWidth, newWidth);
        
        //sonarCamera.pixelRect = new Rect( rect );
    }
}
