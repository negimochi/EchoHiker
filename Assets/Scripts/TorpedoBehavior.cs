using UnityEngine;
using System.Collections;

public class TorpedoBehavior : MonoBehaviour {

    enum Type
    {
        Normal,         // �ʏ�
        Tracking      // �ǔ����[�h
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
    private Rect runningArea;   // �ړ��͈�


    void OnDestroyObject()
    {
        Debug.Log("TorpedoBehaviour.OnDestroy");
        Destroy(gameObject);
    }

    void OnHit()
    {
        // �Փ˂𖳌���
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
            // ���͈͂𒴂�����폜
            OnDestroyObject();
        }
        else
        {
            // �O�ɐi��
            MoveForward();
        }
	}

    private void MoveForward()
    {
        Vector3 vec = speed.current * transform.forward.normalized;
        rigidbody.MovePosition(rigidbody.position + vec * Time.deltaTime);
    }
}
