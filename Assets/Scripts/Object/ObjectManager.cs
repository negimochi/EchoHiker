using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {


    private ArrayList generators = null;
    private RandomGenerator currentGen = null;
    private int current = 0;

	void Start () 
    {
        // �SGenerator�̔z��
//      generators = gameObject.GetComponentsInChildren<RandomGenerator>();
        generators = new ArrayList();
        GameObject enemyObj = GameObject.Find("/Object/EnemyManager");
        GameObject itemObj = GameObject.Find("/Object/ItemManager");
        if (itemObj) generators.Add(itemObj.GetComponent<RandomGenerator>());
        if (enemyObj) generators.Add(enemyObj.GetComponent<RandomGenerator>());
    }
	
	void Update () 
    {
        if (currentGen == null) return;
        if (currentGen.ChildrenNum() == 0)
        {
            // �I�u�W�F�N�g�����[���Ȃ琶���^�C�v��ύX
            Switch();
            // �J�n
            Run();
        }
	}

    private void Run()
    {
        if (currentGen == null) return;

        currentGen.SendMessage("OnStart");
    }

    private void Switch()
    {
        if (generators.Count == 0) return;

        currentGen.SendMessage("OnSuspend");

        current++;
        if (current >= generators.Count) current = 0;
        Debug.Log("current=" + current);
        currentGen = generators[current] as RandomGenerator;
    }

    void OnGameStart()
    {
        if( generators.Count == 0 ) {
            Debug.Log("RandomGenerator is not exists..");
            Application.Quit();
        }

//        foreach (RandomGenerator gen in generators)
//        {
//            gen.BroadcastMessage("OnGameStart", SendMessageOptions.DontRequireReceiver);
//        }

        // �Q�[�����n�߂�
        current = 0;
        currentGen = generators[current] as RandomGenerator;
        Run();
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
