using UnityEngine;
using System.Collections;

public class ValueTest : MonoBehaviour {

    [SerializeField]
    private GameObject gameobj;

	void Start () {

        Generate(-12.0f, -25.0f);
        Generate(-14.0f, 0.0f);
        Generate(-16.0f, 20.0f);
    }

    void Generate( float offset, float value ) {
        Vector3 pos = new Vector3(-20.0f, 0.0f, offset);
        // ÉAÉCÉeÉÄê∂ê¨
        GameObject newItem = Object.Instantiate(gameobj, pos, Quaternion.identity) as GameObject;
        newItem.transform.parent = transform;

        float newTime = Mathf.InverseLerp(newItem.transform.position.x, 20.0f, value);
        Debug.Log(newTime);
        newItem.transform.position = new Vector3(Mathf.Lerp(newItem.transform.position.x, 20.0f, newTime), pos.y, pos.z);
    }
	
	void Update () {
	
	}
}
