using UnityEngine;
using System.Collections;

/// <summary>
/// Notes(音の)の管理。
/// 音が多すぎると難しいし、かといって何もない状態が続くとゲームとして成り立たない。
/// 従って、音数、発音位置を全体的に見渡すものが必要。
///
/// 1. 全体の音の状態を監視する（またはPlayer等から依頼をもらう？）
/// 2. あるアルゴリズムで音の数を判定。
/// 3. 少ない場合はGeneraterにオブジェクトの作成を依頼
/// 
/// </summary>
public class NotesManager : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
