using UnityEngine;
using System.Collections;


// WilfremPのMMDX参照

public class IKSolverScript : MonoBehaviour {
	
	public GameObject target_bone;
	public GameObject[] child_bone;
	public int iteration;
	public float control_weight;
	
	Vector3 prev_position;
	DefaultIKLimitter ik_limit;
	
	// 根本ボーンからCountの数だけ子供方向へ辿れる可能性もあり
	// そうなるとchild_bonesの意味がなくなるので要検証
	
	// プレハブの更新を行ってないのでGameObjectがNULLになる 
	static public void SetAttribute(IKSolverScript scr, GameObject target, GameObject[] childs, int iteration, float weight) {
		scr.target_bone = target;
		scr.child_bone = childs;
		scr.iteration = iteration;
		scr.control_weight = weight;
	}
	
	// Use this for initialization
	void Start () {
		ik_limit = new DefaultIKLimitter();
	}
	
	// ローカル座標をグローバルに変換する 
	void AssignLocalMatrixForTransform(Transform bone, Matrix4x4 local)
	{
		Matrix4x4 glob = bone.localToWorldMatrix;
		glob *= local;
		
		// 座標と回転の更新 
		bone.transform.position = MMDMathf.CreatePositionFromMatrix(glob);
		bone.transform.rotation = MMDMathf.CreateQuaternionFromRotationMatrix(glob);
	}
	
	Matrix4x4 MultipleBoneLocal(Matrix4x4 local, GameObject bone)
	{
		local = bone.transform.localToWorldMatrix;
		local *= bone.transform.parent.worldToLocalMatrix;
		AssignLocalMatrixForTransform(bone.transform.parent, local);
		return local;
	}
	
	// ここは必要かどうかわからない 
	Matrix4x4 UpdateBone(GameObject effector) {
		//子ボーンの更新を行う 
		Matrix4x4 localm = Matrix4x4.identity;
		
		for (int i = this.child_bone.Length-1; i >= 0; i--)
		{
			localm = MultipleBoneLocal(localm, this.child_bone[i]);
			
			// GlobalTransform仮更新 
			// int parentBone = ik.IKChildBones[i].SkeletonHierarchy; 
            // ik.IKChildBones[i].LocalTransform.CreateMatrix(out local); 
            // Matrix.Multiply(ref local, ref BoneManager[parentBone].GlobalTransform, out ik.IKChildBones[i].GlobalTransform); 
		}
		localm = MultipleBoneLocal(localm, effector);
		return localm;
	}
	
	// ベクトルと行列の掛け算 
	Vector3 MultipleVectorAndMatrix(Vector3 v, Matrix4x4 m)
	{
		Vector3 r = new Vector3();
		r.x = r.x * m.m00 + r.y * m.m10 + r.z * m.m20;
		r.y = r.x * m.m01 + r.y * m.m11 + r.z * m.m21;
		r.z = r.x * m.m02 + r.y * m.m12 + r.z * m.m22;
		return r;
	}
	
	void UpdateIKBones() {
		Vector3 local_target_pos = Vector3.zero;
		Vector3 local_effector_pos = Vector2.zero;
		
		// IKTargetBone
		GameObject effector = this.target_bone;		// effectorの言い換え, IK effectorから
		Matrix4x4 local = UpdateBone(effector);
		
		Vector3 target_pos = target_bone.transform.position; // 最初に接続するボーン, targetの言い換え
		
		// 再帰回数の分だけループ
		for (int it = 0; it < this.iteration; it++)
		{
			for (int cnt = 0; cnt < this.child_bone.Length; cnt++)
			{	
				// 子ノードから順番に
				GameObject node = this.child_bone[cnt];
				// TargetBoneの位置が代入されている
				Vector3 effector_pos = effector.transform.position;
				// 注目ノード位置の取得
				Vector3 joint_pos = node.transform.position;
				
				// ノードのワールド座標系から局所座標系に変換 
				Matrix4x4 inv_coord = Matrix4x4.Inverse(node.transform.localToWorldMatrix);
				// 各ベクトルの座標変換を行い、検索中のボーンi基準の座標系にする 
				// (1) 注目ノード→エフェクタ位置へのベクトル(a)(注目ノード) 
				local_effector_pos = MultipleVectorAndMatrix(effector_pos, inv_coord);
				// (2) 基準関節iから目標位置へのベクトル(b)(ボーンi基準座標系)
				local_target_pos = MultipleVectorAndMatrix(target_pos, inv_coord);
				
				// (1) 基準関節からエフェクタ位置への方向ベクトル 
				Vector3 base2effector = Vector3.Normalize(local_effector_pos);
				// (2) 基準関節から目標位置への方向ベクトル 
				Vector3 base2target = Vector3.Normalize(local_target_pos);
				
				// 回転角 
				float rot_dot_prod = Vector3.Dot(base2effector, base2target);
				float rot_angle = Mathf.Acos(rot_dot_prod);
				
				// 回転量制限 
				if (rot_angle > Mathf.PI * this.control_weight * (cnt+1))
					rot_angle = Mathf.PI * this.control_weight * (cnt+1);
				if (rot_angle < -Mathf.PI * this.control_weight * (cnt+1))
					rot_angle = -Mathf.PI * this.control_weight * (cnt+1);
				
				if (!float.IsNaN(rot_angle) && rot_angle > 1.0e-3f)
				{
					// 回転軸 
					Vector3 rot_axis = Vector3.Cross(base2effector, base2target);
					rot_axis.Normalize();
					
					// 関節回転量の補正 
					Quaternion sub_rot = Quaternion.AngleAxis(rot_angle, rot_axis);
					node.transform.rotation = sub_rot * node.transform.rotation;
					//ik_limit.Adjust(node); 
					
					// 関係ノードのグローバル座標更新 
					for (int i = cnt; i >= 0; i--)
					{
						// GlobalTransform更新 
						local *= this.child_bone[i].transform.parent.worldToLocalMatrix;
						AssignLocalMatrixForTransform(this.child_bone[i].transform.parent, local);
					}
					local = effector.transform.worldToLocalMatrix;
					AssignLocalMatrixForTransform(effector.transform.parent, local);
				}
			}
		}
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (this.prev_position != transform.position) {
			//UpdateIKBones();
		}
		this.prev_position = transform.position;	// 一緒だったら処理させない 
	}
}
