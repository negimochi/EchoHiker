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
    private float height = 0.0f;
    [SerializeField]
    private float minTheta = 0.0f;
    [SerializeField]
    private float maxTheta = 360.0f;
    [SerializeField]
    private float minRadius = 100.0f;
    [SerializeField]
    private float maxRadius = 1000.0f;

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
            // �Z�o
            Point point = new Point( -800, -800, 800, 800 );
            point.Polar(Random.value * (maxRadius - minRadius) + minRadius, Random.value * (maxTheta - minTheta) + minTheta);
            Vector3 pos = new Vector3(point.vec.x, height, point.vec.y);
            // �A�C�e������
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;
            newItem.rigidbody.position = pos;
            newItem.transform.parent = transform;
            Debug.Log("generated enemy[" + i + "]=(" + pos.x + "," + pos.z + ")");
            // �Ǘ��p�ɕۑ�
            items.Add(newItem);
        }
	}
	
}
