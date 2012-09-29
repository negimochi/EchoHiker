using UnityEngine;
using System.Collections;

public class SonarCamera : MonoBehaviour {

    [SerializeField]
    private float radius = 0.0f;
    [SerializeField]
    private string sonarOutsideTag = "Sonar";   // ソナーの外側にいるオブジェクト
    [SerializeField]
    private string sonarInsideTag = "SonarInside";    // ソナーの内側にいるオブジェクト


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
//            other.gameObject.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
//        }
//    }

    void OnTriggerStay(Collider other)
    {
        if (Check_Outside2Inside(other.gameObject))
        {
            if (!other.gameObject.renderer.enabled)
            {
                Debug.Log("SonarCamera.OnTriggerEnter");
                other.gameObject.SendMessage("OnSonarInside");
            }
        }
    }
    private bool Check_Outside2Inside(GameObject target)
    {
        // スクリプト内のフラグまでアクセスするのは面倒なのでタグで手を打つ
        if (target.CompareTag(sonarOutsideTag)) {
            target.tag = sonarInsideTag;    // タグを書き換える
            return true;
        }
        return false;
    }


    void OnTriggerExit(Collider other)
    {
        if (Check_Inside2Outside(other.gameObject))
        {
            Debug.Log("SonarCamera.OnTriggerExit");
            other.gameObject.SendMessage("OnSonarOutside");
        }
    }
    private bool Check_Inside2Outside(GameObject target)
    {
        if (target.CompareTag(sonarInsideTag))
        {
            target.tag = sonarOutsideTag;    // タグを書き換える
            return true;
        }
        return false;
    }

    void OnInstantiatedSonarPoint(GameObject target)
    {
        // すでにソナー内にいるかチェックする
        Vector3 pos = new Vector3( transform.position.x, 0.0f, transform.position.z );
        float dist = Vector3.Distance(pos, target.transform.position);
        Debug.Log("OnInstantiatedSonarPoint: dist=" + dist + ", radius=" + radius);
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
