using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

    private RandomGenerator[] generators = null;
    private RandomGenerator currentGen = null;
    private int current = 0;

	void Start () 
    {
        // �SGenerator�̔z��
        generators = gameObject.GetComponentsInChildren<RandomGenerator>();
//        GameObject itemObj = GameObject.Find("/Object/ItemManager");
//        GameObject enemyObj = GameObject.Find("/Object/EnemyManager");
//        if (itemObj)  item  = itemObj.GetComponent<RandomGenerator>();
//        if (enemyObj) enemy = enemyObj.GetComponent<RandomGenerator>();
    }
	
	void Update () 
    {
        if (currentGen == null) return;
        if (currentGen.ChildrenNum() == 0)
        {
            // �I�u�W�F�N�g�����[���Ȃ琶���^�C�v��ύX
            Switch();
            // ����
            Generate();
        }
	}

    private void Generate()
    {
        if (currentGen == null) return;

    }

    private void Switch()
    {
        if (generators.Length == 0) return;
        current++;
        if (generators.Length <= current) current = 0;
        currentGen = generators[current];
    }

    void OnGameStart()
    {
        if( generators.Length == 0 ) {
            Debug.Log("RandomGenerator is not exists..");
            Application.Quit();
        }

        foreach (RandomGenerator gen in generators)
        {
            gen.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
        }

        // �Q�[�����n�߂�B
        currentGen = generators[0];
        Generate();
    }

    void OnGameOver()
    {
        foreach( RandomGenerator gen in generators ) {
            gen.BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);
        }
        // �Q�[�����I���B
        currentGen = null;
    }
}
