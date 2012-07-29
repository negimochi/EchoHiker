using UnityEngine;
using System.Collections;

/// <summary>
/// �����̈ړ�
/// </summary>
public class TorpedoBehavior : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Rect runningArea;   // �L���͈́i���[���h���W�j

	void Start () 
    {
	
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
            // �ʏ�͑O�ɐi��
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

}
