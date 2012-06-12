using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

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

	// Use this for initialization
	void Start () 
    {
        items = new ArrayList();
    }

    public void Generate( int size ) 
    {
        if (itemObject.Length == 0) return;
        for (int i = 0; i < size; i++)
        {
            // �A�C�e���̎�ށA�ʒu�����߂�
            int index = Random.Range(0, itemObject.Length - 1);

            Point point = new Point();
            point.Polar(Random.value * (maxR - minR) + minR, Random.value * ( maxTheta - minTheta ) + minTheta);
            Vector3 pos = new Vector3(point.x, height, point.z);
            // �A�C�e������
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
            // �ۑ����Ă���
            items.Add(newItem);
        }
	}
	
}
