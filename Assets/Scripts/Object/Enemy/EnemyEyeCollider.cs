using UnityEngine;
using System.Collections;

public class EnemyEyeCollider : MonoBehaviour {

    [SerializeField]
    public float insideRadius = 100.0f;

    private float outsideRadius;
    private GameObject parentObj = null;

	void Start () 
    {
        parentObj = gameObject.transform.parent.gameObject;
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider)
        {
            outsideRadius = sphereCollider.radius;
        }
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
        return Mathf.InverseLerp(insideRadius, outsideRadius, dist);
    }

}
