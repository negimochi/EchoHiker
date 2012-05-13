using UnityEngine;
using System.Collections;

/// <summary>
/// オブジェクトを注視するカメラ 
/// 被写体にアサインしてください 
/// </summary>
public class ObjectLookAtCamera : MonoBehaviour {
	
	/// <summary>
	/// 移動させたいカメラ 
	/// </summary>
	public Camera camera = null;
	
	/// <summary>
	/// カメラとオブジェクトとの距離 
	/// </summary>
	public float distance = 10f;
	
	/// <summary>
	/// カメラの位置調整
	/// </summary>
	public Vector3 adjustmentCameraPosition;
	
	/// <summary>
	/// 注視点の調整 
	/// </summary>
	public Vector3 adjustmentLookAtPosition;
	
	// Use this for initialization
	void Start () {
		// カメラが空になっているときはメインカメラで 
		if (camera == null)
			camera = Camera.mainCamera;
	}
	
	// Update is called once per frame
	void Update () {
		// オブジェクトの向きにベクトルを伸ばす 
		Vector3 forward = -transform.forward * distance;
		Vector3 lookatPos, cameraPos;
		
		// 位置調整ベクトルの位置をモデルの向きに合わせる 
		TransformCoordinate(out lookatPos, out cameraPos);
		
		// カメラをオブジェクトの後方に持ってくる 
		camera.transform.position = transform.position + forward + cameraPos;
		
		// カメラをオブジェクトに注視させる 
		Vector3 lookat = transform.position + lookatPos - camera.transform.position;
		camera.transform.rotation = Quaternion.LookRotation(lookat.normalized, transform.up);
	}
	
	/// <summary>
	/// 調整ベクトルの座標系を変換 
	/// </summary>
	void TransformCoordinate(out Vector3 lookatPos, out Vector3 cameraPos) {
		// モデルの位置や姿勢などを決める行列を取得 
		Matrix4x4 inverse = transform.localToWorldMatrix;
		
		// 逆行列 = モデルの向きとか位置とかを決める行列 
		// この行列をかけることでローカルな位置に戻す 
		lookatPos = inverse.MultiplyVector(adjustmentLookAtPosition);
		cameraPos = inverse.MultiplyVector(adjustmentCameraPosition);
		
		/*
		 * 具体的な解説 
		 * 変換行列（逆行列）とはモデルの姿勢や位置や大きさなどを決める行列 
		 * これをあるベクトルや位置に掛けてあげることで 
		 * そのベクトルや位置をあるモデルから見た状態に変換することができる 
		 * 
		 * 普通のVector3(0,0,1)なら北から1メートルと解釈できるかもしれない 
		 * でも, あるモデルの正面から1メートルという解釈に直したいときに 
		 * このモデルにかかった逆行列を取得して掛け合わせれば 
		 * Vector3(0,0,1)はあるの正面から1メートル, という解釈に直すことができる 
		 * 
		 * 他にも利用法はいろいろあるけど今回ではそんな使い方をしています 
		 */
	}
}
