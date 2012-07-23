using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour
{
    [SerializeField]
    public float waitTime = 5.0f;
    [SerializeField]
    private int scoreMax = 1000;
    [SerializeField]
    public int scoreMin = 50;
    [SerializeField]
    public int step = 10;

    private int scoreValue = 100;
    GameObject uiObj = null;

    void Start()
    {
        uiObj = GameObject.Find("/UI");
        // �����Ŋl���|�C���g���U�炷
        scoreValue = scoreMax;
        StartCoroutine("ChangeScoreValue");
    }

    private IEnumerator ChangeScoreValue()
    {
        yield return new WaitForSeconds(waitTime);

        scoreValue = Mathf.Clamp(scoreValue - step, scoreMin, scoreMax);
        if (scoreValue > scoreMin) 
        {
            StartCoroutine("ChangeScoreValue");
        }
    }

    void OnDestroyObject()
    {
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject, SendMessageOptions.DontRequireReceiver);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))   // �v���C���[������
        {
            // �X�R�A���Z
            if (uiObj) uiObj.BroadcastMessage("OnGetScore", scoreValue, SendMessageOptions.DontRequireReceiver);
            // �q�b�g��̎����̏���
            BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
            // Collider��؂�
            collider.enabled = false;
        }
    }

}
