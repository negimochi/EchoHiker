using UnityEngine;
using System.Collections;

public class SonarCamera : MonoBehaviour {

    [SerializeField]
    private float radius = 0.0f;

    void Awake()
    {
        // カメラとCollider半径を揃えておく
        radius = camera.orthographicSize;
        Debug.Log(Time.time + ": SonarCamera.Awake");
    }

    void Start()
    {

        SphereCollider shereCollider = GetComponent<SphereCollider>();
        if (shereCollider)
        {
            shereCollider.radius = radius;
        }
    }

    // Enterではとりのがしが発生する場合がある
//    void OnTriggerEnter(Collider other)
//    {
//        if (CheckObject(other.gameObject))
//        {
//           Debug.Log("SonarCamera.OnTriggerEnter");
//            other.gameObject.BroadcastMessage("OnSonarInside");
//        }
//    }

    // Stayで代用する
    void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        if (CheckOutsideSonar(target))
        {
            Debug.Log("SonarCamera.OnTriggerEnter");
            target.SendMessage("OnSonarInside");
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (CheckInsideSonar(target))
        {
            Debug.Log("SonarCamera.OnTriggerExit");
            target.SendMessage("OnSonarOutside");
        }
    }

    private bool CheckInsideSonar(GameObject target)
    {
        if (!target.CompareTag("Sonar")) return false;

        Debug.Log("CheckInsideSonar[" + target.transform.parent.gameObject.name + "]");
        ColorFader fader = target.GetComponent<ColorFader>();
        if (fader) return fader.SonarInside();
        return false;
    }

    private bool CheckOutsideSonar(GameObject target)
    {
        if (!target.CompareTag("Sonar")) return false;

        Debug.Log("CheckOutsideSonar[" + target.transform.parent.gameObject.name + "]");
        ColorFader fader = target.GetComponent<ColorFader>();
        if (fader) return (!fader.SonarInside());
        return false;
    }

    void OnInstantiatedSonarPoint(GameObject target)
    {
        // すでにソナー内にいるかチェックする
        Vector3 pos = new Vector3( transform.position.x, 0.0f, transform.position.z );
        float dist = Vector3.Distance(pos, target.transform.position);
        Debug.Log("OnInstantiatedSonarPoint[" + target.transform.parent.gameObject.name + "]: dist=" + dist + ", radius=" + radius);
        if (dist <= radius)
        {
            target.SendMessage("OnSonarInside");
        }
        else {
            target.SendMessage("OnSonarOutside");
        }
    }

    public float Radius() 
    {
        return radius;
    } 

    // 表示位置調整
	public void SetRect( Rect rect )
    {
        Debug.Log("SetRect:" + rect);
        camera.pixelRect = new Rect(rect.x, rect.y, rect.width, rect.height);

        // カメラ表示領域をテクスチャに内接させる場合
        //float r = rect.width * 0.5f;
        //float newWidth = r * Mathf.Pow(2.0f,0.5f);
        //float sub = (rect.width - newWidth)*0.5f;
        //camera.pixelRect = new Rect(rect.x + sub, rect.y + sub, newWidth, newWidth);
        
        //sonarCamera.pixelRect = new Rect( rect );
    }
}
