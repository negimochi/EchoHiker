using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private float itemDelaytime = 1.0f;
    [SerializeField]
    private float enemyDelaytime = 30.0f;

    private RandomGenerator itemGenerator;
    private RandomGenerator enemyGenerator;

	void Start () 
    {
        // ÉVÅ[Éhèâä˙âª
        Random.seed = (int)(System.DateTime.Now.TimeOfDay.TotalMilliseconds);

        GameObject itemObj = GameObject.Find("/Object/ItemGenerator");
        itemGenerator = itemObj.GetComponent<RandomGenerator>();

        GameObject enemyObj = GameObject.Find("/Object/EnemyGenerator");
        enemyGenerator = enemyObj.GetComponent<RandomGenerator>();
    }
	
	void Update () 
    {
        /*
        if (!itemGenerator.SizeOver())
        {
            itemGenerator.Auto(itemDelaytime);
        }
        if (!enemyGenerator.SizeOver())
        {
            enemyGenerator.Auto(enemyDelaytime);
        }
         */
    }
}
