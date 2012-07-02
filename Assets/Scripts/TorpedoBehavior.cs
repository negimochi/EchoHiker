using UnityEngine;
using System.Collections;

public class TorpedoBehavior : MonoBehaviour {

    enum Type
    {
        Normal,         // 通常
        Tracking      // 追尾モード
    };
    [SerializeField]
    private Type type;

    [System.Serializable]
    public class SpeedValue
    {
        public float current = 1.0f;
        [SerializeField]
        private float max = 10.0f;
        [SerializeField]
        private float step = 0.5f;

        public void Change(float value)
        {
            current += value * step;
            if (current < 0.0f) current = 0.0f;
            else if (current > max) current = max;
        }

        public void Stop() { current = 0.0f; }
    };
    [SerializeField]
    private SpeedValue speed;

    [SerializeField]
    private Rect runningArea;   // 移動範囲


    void OnDestroyObject()
    {
        Debug.Log("TorpedoBehaviour.OnDestroy");
        Destroy(gameObject);
    }

    void OnHit()
    {
        // 衝突を無効化
        SphereCollider collider = GetComponent<SphereCollider>();
        if (collider) collider.enabled = false;
    }

	// Use this for initialization
	void Start () {
	
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
            // 前に進む
            MoveForward();
        }
	}

    private void MoveForward()
    {
        Vector3 vec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
    }
}
