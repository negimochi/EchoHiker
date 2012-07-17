using UnityEngine;
using System.Collections;

public class ActiveSonar : MonoBehaviour {

    [SerializeField]
    private float maxRadius = 250.0f;

    private float currentTime = 0.0f;
    private bool search = false;

    private GameObject player = null;
    private RandomGenerator enemy = null;
    private RandomGenerator item  = null;
    private SonarEffect effect = null;

	void Start () 
    {
        effect = GetComponent<SonarEffect>();
        player = GameObject.Find("/Player");
        GameObject enemyObj = GameObject.Find("/Object/EnemyManager");
        if (enemyObj) 
        {
            enemy = enemyObj.GetComponent<RandomGenerator>();
        }
        GameObject itemObj = GameObject.Find("/Object/ItemManager");
        if (itemObj)
        {
            item = itemObj.GetComponent<RandomGenerator>();
        }
	}
	
	void Update () 
    {
        float effectDist = Mathf.Lerp(0.0f, maxRadius, effect.Value());
        if (enemy) Search(enemy.ChildrenArray(), effectDist);
        if (item) Search(item.ChildrenArray(), effectDist);
    }

    void Search(GameObject[] array, float effectDist) 
    {
        int size = array.Length;
        for (int i = 0; i < size; i++ )
        {
            float dist = Vector3.Distance(array[i].transform.position, transform.position);
            if (effectDist > dist)
            {
                array[i].BroadcastMessage("OnSonar", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
