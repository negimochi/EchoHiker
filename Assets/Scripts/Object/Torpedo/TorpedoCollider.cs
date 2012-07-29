using UnityEngine;
using System.Collections;

/// <summary>
/// �����̏Փ�
/// </summary>
public class TorpedoCollider : MonoBehaviour {

    public enum OwnerType {
        Player,
        Enemy
    };
    private OwnerType owner;

    [SerializeField]
    private float delayTime = 2.0f;
    [SerializeField]
    private int damegeValue = 1;

    [System.Serializable]
    public class Explosion
    {
        [SerializeField]
        private float force = 100.0f;
        [SerializeField]
        private float upwardsModifier = 0.0f;
        [SerializeField]
        private ForceMode mode = ForceMode.Impulse;

        private float radius = 3.0f;
            
        public void Add(Rigidbody target, Vector3 pos) 
        {
            target.AddExplosionForce(force, pos, radius, upwardsModifier, mode);
        }
        public void SetRadius(float value) { radius = value; }
    };
    [SerializeField]
    Explosion explosion; 

    private GameObject uiObj = null;

	void Start () 
    {
        uiObj = GameObject.Find("/UI");

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider) explosion.SetRadius( sphereCollider.radius );

        // ���˂��������Ƀq�b�g����̂ŁA���ː��b�����Փ˔��肵�Ȃ��B
        collider.enabled = false;
        StartCoroutine("Delay");
	}

    void OnCollisionEnter(Collision collision)
    {
        CollisionCheck( collision.gameObject );
    }
    void OnCollisionStay(Collision collision)
    {
        CollisionCheck( collision.gameObject );
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);

        collider.enabled = true;
        Debug.Log("Wait EndCoroutine");
    }

    private void CollisionCheck(GameObject target)
    {
        bool hit = false;
        hit |= CheckPlayer(target);
        hit |= CheckEnemy(target);
        hit |= CheckTorpedo(target);
        if( hit ) {
            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider������
            collider.enabled = false;
        }
    }

    private bool CheckTorpedo(GameObject target)
    {
        if (target.CompareTag("Torpedo"))
        {
            Debug.Log("CheckTorpedo");
            // ����̋����Ƀq�b�g
            //target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            return true;
        }
        return false;
    }

    private bool CheckPlayer(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            Debug.Log("CheckPlayer");
            // �Ռ���^����
            explosion.Add( target.rigidbody, transform.position );
 
            // �_���[�W�ʒm
            if (uiObj) uiObj.BroadcastMessage("OnDamage", damegeValue, SendMessageOptions.DontRequireReceiver);
            return true;
        }
        return false;
    }

    private bool CheckEnemy(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            Debug.Log("CheckEnemy");
            if (owner == OwnerType.Player)
            {
                // �����̋������G�Ƀq�b�g�����Ƃ������A�G�̎����Ă���X�R�A�����Z����ʒm
                target.SendMessage("OnGetScore", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                // �G�Ƀq�b�g�ʒm��������
                target.BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// �����𔭎˂����I�[�i�[���Z�b�g
    /// </summary>
    /// <param name="type"></param>
    public void SetOwner(OwnerType type) { owner = type; }
    /// <summary>
    /// �_���[�W�ʂ��Z�b�g�B�ʏ킷��K�v�Ȃ�
    /// </summary>
    /// <param name="value"></param>
    public void SetDamageValue(int value) { damegeValue = value; }

}
