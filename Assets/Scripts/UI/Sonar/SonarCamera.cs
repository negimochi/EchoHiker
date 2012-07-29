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
    // ����݂�̂͂�����ƁE�E�E
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
        // ���łɃ\�i�[���ɂ��邩�`�F�b�N����
        Vector3 pos = new Vector3( transform.position.x, 0.0f, transform.position.z );
        if (Vector3.Distance(pos, target.transform.position) <= radius)
        {
            target.BroadcastMessage("OnSonarInside", SendMessageOptions.DontRequireReceiver);
        }
        else {
            target.BroadcastMessage("OnSonarOutside", SendMessageOptions.DontRequireReceiver);
        }
    }

    // �\���ʒu����
	public void SetRect( Rect rect )
    {
        camera.pixelRect = new Rect(rect.x, rect.y, rect.width, rect.height);

        // �J�����\���̈���e�N�X�`���ɓ��ڂ�����ꍇ
        //float r = rect.width * 0.5f;
        //float newWidth = r * Mathf.Pow(2.0f,0.5f);
        //float sub = (rect.width - newWidth)*0.5f;
        //camera.pixelRect = new Rect(rect.x + sub, rect.y + sub, newWidth, newWidth);
        
        //sonarCamera.pixelRect = new Rect( rect );
    }
}
