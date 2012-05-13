using UnityEngine;
using System.Collections;

/// <summary>
/// 上下に首振り前後移動コントローラ 
/// マウス利用 
/// </summary>
public class SpaceWorldController : MonoBehaviour {
	
	/// <summary>
	/// 首振り速度の倍率 
	/// </summary>
	public float neckSwingSpeed;
	
	/// <summary>
	/// 移動速度 
	/// </summary>
	public float movingSpeed;
	
	/// <summary>
	/// 首振りは逆向きにするかどうか 
	/// </summary>
	public bool reverse = false;
	
	/// <summary>
	/// 矢印で移動するかどうか 
	/// </summary>
	public bool useArrow = true;
	
	/// <summary>
	/// WASDキーで移動するかどうか 
	/// </summary>
	public bool useWASD = true;
	
	/// <summary>
	/// 前フレームのマウスの位置 
	/// </summary>
	Vector3 prevPos;
	
	// Use this for initialization
	void Start () {
		prevPos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		Translate();
		Rotate();
		
		// 最後に現在のフレームのマウス位置をセット 
		prevPos = Input.mousePosition;
	}
	
	/// <summary>
	/// 移動 
	/// </summary>
	void Translate() {
		Vector3 transformVector = Vector3.zero;
		
		// キーボードの状態を取得 
		if (Input.GetKey(KeyCode.W) & useWASD ||
		    Input.GetKey(KeyCode.UpArrow) & useArrow)
			transformVector += transform.forward * movingSpeed;
		
		if (Input.GetKey(KeyCode.S) & useWASD ||
		    Input.GetKey(KeyCode.DownArrow) & useArrow)
			transformVector -= transform.forward * movingSpeed;
		
		if (Input.GetKey(KeyCode.D) & useWASD ||
		    Input.GetKey(KeyCode.RightArrow) & useArrow)
			transformVector += transform.right * movingSpeed;
		
		if (Input.GetKey(KeyCode.A) & useWASD ||
		    Input.GetKey(KeyCode.LeftArrow) & useArrow)
			transformVector -= transform.right * movingSpeed;
		
		// ここで移動させる 
		transform.position += transformVector;
	}
	
	/// <summary>
	/// 回転 
	/// </summary>
	void Rotate() {
		// マウスが移動した量を取得 
		Vector3 direction = Input.mousePosition - prevPos;
		
		// directionから見て右向きのベクトルを生成 
		Vector3 right = Vector3.Cross(direction.normalized, Vector3.forward);
		right = reverse ? -right : right;	// 逆向き回転が有効だった場合 
		
		// directionの右向きを軸に首を振る 
		transform.Rotate(right, direction.magnitude * neckSwingSpeed);
	}
}



