using UnityEngine;
using System.Collections;

public class SonarCamera : MonoBehaviour {

    private float radius = 0.0f;

    void Start()
    {
        // カメラとCollider半径を揃えておく
        radius = camera.orthographicSize;
        SphereCollider shereCollider = GetComponent<SphereCollider>();
        if (shereCollider)
        {
            shereCollider.radius = radius;
        }

        // 初期配置されていた場合の対応
        GameObject enemy = GameObject.Find("/Object/EnemyManager");
        if (enemy) {
            ArrayList enemyArr = enemy.GetComponent<RandomGenerator>().Children();
//            foreach( enemyArr ) ;
        }
        GameObject item = GameObject.Find("/Object/ItemManager");
        if (item)
        {
            RandomGenerator itemGen = enemy.GetComponent<RandomGenerator>();
        }
//        sonarArray
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckObject(other.gameObject))
        {
            other.gameObject.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
        }
    }
    // 毎回みるのはちょっと・・・
//    void OnTriggerStay(Collider other)
//    {
//    }
    void OnTriggerExit(Collider other)
    {
        if (CheckObject(other.gameObject))
        {
            other.gameObject.BroadcastMessage("OnSonarOutside", SendMessageOptions.DontRequireReceiver);
        }
    }

    private bool CheckObject( GameObject target )
    {
        return (target.CompareTag("Sonar")) ? true : false;
    }

    void OnInstantiatedChild(GameObject target)
    {
        // すでにソナー内にいるかチェックする
        Vector3 pos = new Vector3( transform.position.x, 0.0f, transform.position.z );
        if (Vector3.Distance(pos, target.transform.position) <= radius)
        {
            target.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
        }
        else {
            target.BroadcastMessage("OnSonarOutside", SendMessageOptions.DontRequireReceiver);
        }
    }

    // 表示位置調整
	public void SetRect( Rect rect )
    {
        camera.pixelRect = new Rect(rect.x, rect.y, rect.width, rect.height);

        // カメラ表示領域をテクスチャに内接させる場合
        //float r = rect.width * 0.5f;
        //float newWidth = r * Mathf.Pow(2.0f,0.5f);
        //float sub = (rect.width - newWidth)*0.5f;
        //camera.pixelRect = new Rect(rect.x + sub, rect.y + sub, newWidth, newWidth);
        
        //sonarCamera.pixelRect = new Rect( rect );
    }
}
