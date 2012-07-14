using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    [SerializeField]
    private int scoreMax = 500;
    [SerializeField]
    public int scoreMin = 10;

    private int scoreValue = 100;

    private GameObject ui = null;

    void Start() 
    {
        ui = GameObject.Find("/UI");
    }

    private void OnGetScore()
    {
        Debug.Log("OnGetScore");
        // �X�R�A�l�𑗂�
        ui.BroadcastMessage("OnGetScore", scoreValue);

        // �����Ƀq�b�g����
        BroadcastMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    }
}
