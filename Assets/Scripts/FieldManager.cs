using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    private RandomGenerator item;
    private RandomGenerator enemy;
    private GameObject itemObj;
    private GameObject enemyObj;

	void Start () 
    {
        // ÉVÅ[Éhèâä˙âª
        Random.seed = (int)(System.DateTime.Now.TimeOfDay.TotalMilliseconds);

        itemObj = GameObject.Find("/Object/ItemManager");
        item = itemObj.GetComponent<RandomGenerator>();

        enemyObj = GameObject.Find("/Object/EnemyManager");
        enemy = enemyObj.GetComponent<RandomGenerator>();
    }

}
