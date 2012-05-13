using UnityEngine;
using System.Collections;

/// <summary>
/// バイオ方式で水平移動するコントローラー 
/// </summary>
public class HorizontalController : MonoBehaviour {
	
	/// <summary>
	/// 前進速度 
	/// </summary>
	public float stepSpeed = 0.1f;
	
	/// <summary>
	/// 旋回速度 
	/// </summary>
	public float turnSpeed = 0.1f;
	
	/// <summary>
	/// 入力に矢印キーをつかうか 
	/// </summary>
	public bool useArrow = true;
	
	/// <summary>
	/// 入力にWASDを使うか 
	/// </summary>
	public bool useWASD = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 入力認識制限 
		// この数値以下の入力は無視される 
		const float limit = 0.01f;
		
		UpDown();
		LeftRight();
	}
	
	/// <summary>
	/// 上下キーの入力 
	/// </summary>
	void UpDown() {
		float PM = 0;
		
		// ここで入力チェック 
		if (Input.GetKey(KeyCode.UpArrow) & useArrow ||
		    Input.GetKey(KeyCode.W) & useWASD)
			PM += 1.0f;
		
		if (Input.GetKey(KeyCode.DownArrow) & useArrow ||
		    Input.GetKey(KeyCode.S) & useWASD)
			PM -= 1.0f;
		
		// ここで移動 
		if (PM != 0) 
			transform.Translate(transform.forward * (PM * stepSpeed));
	}
	
	/// <summary>
	/// 左右キーの入力 
	/// </summary>
	void LeftRight() {
		float PM = 0;
		
		// ここで入力チェック 
		if (Input.GetKey(KeyCode.RightArrow) & useArrow ||
		    Input.GetKey(KeyCode.D) & useWASD)
			PM += 1.0f;
		
		if (Input.GetKey(KeyCode.LeftArrow) & useArrow ||
		    Input.GetKey(KeyCode.A) & useWASD)
			PM -= 1.0f;
		
		// ここで回転 
		if (PM != 0)
			transform.Rotate(Vector3.up, turnSpeed * PM);
	}
}
