using UnityEngine;
using System.Collections;

public class RandomRectGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] itemObject;
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;

    public ArrayList items;

	void Start () 
    {
        items = new ArrayList();
    }

    public void Generate(int size)
    {
        Vector3 sub = endPos - startPos;
        for (int i = 0; i < size; i++)
        {
            // アイテムの種類、位置を決める
            int index = Random.Range(0, itemObject.Length - 1);
            Vector3 pos = new Vector3(Random.value * sub.x + startPos.x, Random.value * sub.y + startPos.y, Random.value * sub.z + startPos.z);
            // アイテム生成
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
            // 管理用に保存
            items.Add(newItem);
        }
	}
	
}
