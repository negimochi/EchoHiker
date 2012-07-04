using UnityEngine;
using System.Collections;

public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float waitTime = 2.0f;
    [SerializeField]
    private int damegeValue = 1;
    [SerializeField]
    private int enamyDestroyScore = 100;

    private GameObject ui = null;

	void Start () 
    {
        ui = GameObject.Find("/UI");
        // ���˂̏u�Ԃ́A���@�ɂԂ���̂�Wait������
        collider.enabled = false;
        StartCoroutine("Wait");
	}

    void OnTriggerEnter(Collider other)
    {
        CheckPlayer(other.gameObject);
        CheckEnemy(other.gameObject);
        CheckTorpedo(other.gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        CheckPlayer(other.gameObject);
        CheckEnemy(other.gameObject);
        CheckTorpedo(other.gameObject);
    }


    public void SetOwner(OwnerType type)
    {
        owner = type;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        collider.enabled = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CheckTorpedo(GameObject target)
    {
        if (target.CompareTag("Torpedo"))
        { 
            // ����̋����Ƀq�b�g
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider������
            collider.enabled = false;
        }
    }

    private void CheckPlayer(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            Vector3 vec = new Vector3(transform.position.x, transform.position.y, transform.position.z + sphereCollider.radius);
            target.rigidbody.AddExplosionForce(100.0f, vec, 10.0f, 3.0f, ForceMode.Impulse);

            // �_���[�W�ʒm
            ui.SendMessage("OnDamage", damegeValue, SendMessageOptions.DontRequireReceiver);
            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider������
            collider.enabled = false;
        }
    }
    private void CheckEnemy(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            if (owner == OwnerType.Player)
            {
                // �����̋������G�Ƀq�b�g�B�|�C���g�ʒm
                ui.SendMessage("OnDestroyEnemy", enamyDestroyScore);
            }
            // �G�Ƀq�b�g�ʒm
            target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider������
            collider.enabled = false;
        }
    }
}
