using UnityEngine;
using System.Collections;

/// <summary>
/// �v���C���[���S�Ƀ����_������
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
            // �A�C�e���̎�ށA�ʒu�����߂�
            int index = Random.Range(0, itemObject.Length - 1);

            // ��Z�o
            Point point = new Point();
            point.Polar(Random.value * (maxR - minR) + minR, Random.value * (maxTheta - minTheta) + minTheta);

            // �A�C�e������
            GameObject newItem = Object.Instantiate(itemObject[index]) as GameObject;
            newItem.transform.position = new Vector3(point.x, height, point.y); 
            newItem.transform.parent = transform;
            Debug.Log("generated item[" + i + "]=" + newItem.name);
            // �Ǘ��p�ɕۑ�
            items.Add(newItem);
        }
	}
	
}
