using UnityEngine;
using System.Collections;

public class TorpedoBehavior : MonoBehaviour {

    enum Type
    {
        Normal,         // 通常
        Tracking      // 追尾モード
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
