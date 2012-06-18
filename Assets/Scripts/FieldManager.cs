using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private int enemyAddSize = 1;     // ��x�ő�����G�̐�
    [SerializeField]
    private int goldAddSize = 1;      // ��x�ő�����A�C�e��

    [SerializeField]
    private int maxEnemySize = 2;     // �t�B�[���h���ɑ��݂ł���ő�G��
    [SerializeField]
    private int maxGoldSize = 5;      // �t�B�[���h���ɑ��݂ł���ő�A�C�e���l

    [SerializeField]
    private float maxItemDelaytime = 1.0f;
    [SerializeField]
    private float maxEnemyDelaytime = 30.0f;

//    private RandomCircleGenerator goldGenerator;
    private RandomRectGenerator goldGenerator;
    private RandomCircleGenerator enemyGenerator;
//    private RandomRectGenerator enemyGenerator;

    private float itemTimeCount;
    private float enemyTimeCount;

	void Start () 
    {
        // �V�[�h������
        Random.seed = (int)(System.DateTime.Now.TimeOfDay.TotalMilliseconds);

        GameObject itemObj = GameObject.Find("/Object/ItemGenerator");
//        goldGenerator = itemObj.GetComponent<RandomCircleGenerator>();
        goldGenerator = itemObj.GetComponent<RandomRectGenerator>();
        itemTimeCount = maxItemDelaytime;

        GameObject enemyObj = GameObject.Find("/Object/EnemyGenerator");
        enemyGenerator = enemyObj.GetComponent<RandomCircleGenerator>();
//        enemyGenerator = enemyObj.GetComponent<RandomRectGenerator>();
        enemyTimeCount = maxEnemyDelaytime;
    }
	
	void Update () 
    {
        if (goldGenerator.items.Count < maxGoldSize)
        {
            itemTimeCount += Time.deltaTime;
            if (itemTimeCount > maxItemDelaytime)
            {
                AddGold();
            }
        }
        if (enemyGenerator.items.Count < maxGoldSize)
        {
            enemyTimeCount += Time.deltaTime;
            if (enemyTimeCount > maxEnemyDelaytime)
            {
                AddEnemey();
            }
        }
    }

    private void AddGold()
    {
        if (goldGenerator)
        {
            goldGenerator.Generate(goldAddSize);
        }
        itemTimeCount = 0.0f;
    }

    private void AddEnemey()
    {
        if (enemyGenerator)
        {
            enemyGenerator.Generate(enemyAddSize);
        }
        enemyTimeCount = 0.0f;
    }
}
