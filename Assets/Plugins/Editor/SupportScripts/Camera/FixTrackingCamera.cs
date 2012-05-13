using UnityEngine;
using System.Collections;

/// <summary>
/// ある位置からオブジェクトに注視しつつ, 向きを固定したまま移動するカメラ 
/// プレイヤーなどの被写体にアサインすること 
/// </summary>
public class FixTrackingCamera : MonoBehaviour {
	
	/// <summary>
	/// 利用したいカメラ 
	/// </summary>
	public Camera camera = null;
	
	/// <summary>
	/// オブジェクトに対するカメラの位置 
	/// </summary>
	public Vector3 cameraPos = new Vector3(0,0,10);
	
	/// <summary>
	/// カメラの調整位置 
	/// </summary>
	public Vector3 adjustPos = new Vector3(0,0,0);
	
	// Use this for initialization
	void Start () {
		// カメラの初期設定 
		if (camera == null) camera = Camera.mainCamera;
		camera.transform.position = cameraPos + transform.position;
		
		// カメラの向きを調整する 
		Vector3 forward = (transform.position) - camera.transform.position;
		camera.transform.rotation = Quaternion.LookRotation(forward.normalized, Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		// 調整位置をローカルに変換 
		Matrix4x4 inv = camera.transform.localToWorldMatrix;
		Vector3 adjust = inv.inverse.MultiplyVector(adjustPos);
		
		// カメラの位置合わせ, 常に平行に移動する 
		camera.transform.position = transform.position + cameraPos + adjust;
	}
}
