using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] itemObject;
    [SerializeField]
    private int minAngle;
    [SerializeField]
    private int lines;
    [SerializeField]
    private float height;
    [SerializeField]
    private Point inside;
    [SerializeField]
    private Point outside;

    public Point side;

	// Use this for initialization
	void Start () {
        Random.seed = (int)(System.DateTime.Now.TimeOfDay.TotalMilliseconds);
        Debug.Log( "seed=" + Random.seed );
        Point sub = new Point();
        sub.Polar(outside.r - inside.r, outside.theta - inside.r );
        for (int i = 0; i < lines; i++) {
            // アイテムの種類、位置を決める
            int index = Random.Range(0, itemObject.Length - 1);
            Point point = new Point();
            point.Polar(Random.value * sub.r + inside.r, Random.value * sub.theta + inside.theta);
            Vector3 pos = new Vector3(point.x, height, point.y);
            // アイテム生成
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
        }
	}
	
}
