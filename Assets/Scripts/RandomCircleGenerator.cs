using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤー中心にランダム生成
/// </summary>
public class RandomCircleGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject[] itemObject;
    [SerializeField]
    private float height;
    [SerializeField]
    private float minTheta = 0.0f;
    [SerializeField]
    private float maxTheta = 360.0f;
    [SerializeField]
    private float minR = 100.0f;
    [SerializeField]
    private float maxR = 1000.0f;

    public ArrayList items;
    private GameObject target;

	// Use this for initialization
	void Start () 
    {
        items = new ArrayList();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void Generate( int size ) 
    {
        if (itemObject.Length == 0) return;
        for (int i = 0; i < size; i++)
        {
            // アイテムの種類、位置を決める
            int index = Random.Range(0, itemObject.Length - 1);

            // 一算出
            Point point = new Point();
            point.Polar(Random.value * (maxR - minR) + minR, Random.value * (maxTheta - minTheta) + minTheta);

            // アイテム生成
            GameObject newItem = Object.Instantiate(itemObject[index]) as GameObject;
            newItem.transform.position = new Vector3(point.x, height, point.y); 
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
            // 管理用に保存
            items.Add(newItem);
        }
	}
	
}
