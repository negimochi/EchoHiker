using UnityEngine;
using System.Collections;

/// <summary>
/// 魚雷の移動
/// </summary>
public class TorpedoBehavior : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Rect runningArea;   // 有効範囲（ワールド座標）

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (!runningArea.Contains(new Vector2(transform.position.x, transform.position.z)))
        {
            // 可動範囲を超えたら削除
            OnDestroyObject();
        }
        else
        {
            // 通常は前に進む
            MoveForward();
        }
	}

    private void MoveForward()
    {
        Vector3 vec = speed * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
    }

    void OnDestroyObject()
    {
        Debug.Log("TorpedoBehaviour.OnDestroy");
        Destroy(gameObject);
    }

    public void SetSpeed( float speed_ ) { speed = speed_; }

}
