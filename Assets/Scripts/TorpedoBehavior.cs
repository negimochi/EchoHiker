using UnityEngine;
using System.Collections;

public class TorpedoBehavior : MonoBehaviour {

    enum Type
    {
        Normal,         // �ʏ�
        Tracking      // �ǔ����[�h
    };
    [SerializeField]
    private Type type;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
