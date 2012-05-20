using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] itemObject;
    [SerializeField]
    private int lines;
    [SerializeField]
    private float height;
    [SerializeField]
    private float minTheta;
    [SerializeField]
    private float maxTheta;
    [SerializeField]
    private float minR;
    [SerializeField]
    private float maxR;

	// Use this for initialization
	void Start () {
        Random.seed = (int)(System.DateTime.Now.TimeOfDay.TotalMilliseconds);
        Generate();
    }
    void Generate() {
        Debug.Log( "seed=" + Random.seed );
        Point sub = new Point();
        sub.Polar(maxR - minR, maxTheta - minTheta );
        for (int i = 0; i < lines; i++) {
            // アイテムの種類、位置を決める
            int index = Random.Range(0, itemObject.Length - 1);
            Point point = new Point();
            point.Polar(Random.value * sub.r + minR, Random.value * sub.theta + minTheta);
            Vector3 pos = new Vector3(point.x, height, point.z);
            // アイテム生成
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
        }
	}
	
}
