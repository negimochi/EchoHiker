using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    [SerializeField]
    private int scoreMax = 500;
    [SerializeField]
    public int scoreMin = 10;

    private int scoreValue = 100;

    private GameObject uiObj = null;

    void Start() 
    {
        uiObj = GameObject.Find("/UI");
    }

    private void OnGetScore()
    {
        Debug.Log("OnGetScore");
        // �X�R�A�l�𑗂�
        uiObj.BroadcastMessage("OnGetScore", scoreValue);

        // �����Ƀq�b�g����
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }
}
