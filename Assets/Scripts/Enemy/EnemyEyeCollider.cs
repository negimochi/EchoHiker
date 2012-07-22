using UnityEngine;
using System.Collections;

public class EnemyEyeCollider : MonoBehaviour {

    [SerializeField]
    public float marginRadius = 100.0f;

    private float oldDistRate;
    private float radius;
    private GameObject parentObj = null;

	void Start () 
    {
        parentObj = gameObject.transform.parent.gameObject;
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider)
        {
            radius = sphereCollider.radius;
        }
        oldDist = radius;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        float t = GetDistanceRate(other.gameObject);
        parentObj.SendMessage("OnStayPlayer", t, SendMessageOptions.DontRequireReceiver);
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("TrigExit:" + Time.time);
        parentObj.SendMessage("OnExitPlayer", SendMessageOptions.DontRequireReceiver);
    }

    float GetDistanceRate(GameObject target)
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        float rate = Mathf.InverseLerp(marginRadius, radius, dist);


        return ;
    }

}
