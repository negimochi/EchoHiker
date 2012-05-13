using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// WilfremPのMMDX参照

public class RotationLimit
{
	public float[] MaxRot;	// 最大回転 
	public float[] MinRot;	// 最少回転 
	public bool[] Mirror;	// 角度の反射調整機能フラグ 
	public float[] Restitution;	// 角度の反射調整の反発係数 
	public float[] Stickness;	// 角速度の粘性係数, 解が飛ぶのを防ぐために設定 
	
	public RotationLimit()
	{
		MaxRot = new float[3];
		MinRot = new float[3];
		Mirror = new bool[3];
		Restitution = new float[3];
		for (int i = 0; i < 3; i++)
		{
			MaxRot[i] = Mathf.PI;
			MinRot[i] = -Mathf.PI;
			Mirror[i] = false;
			Restitution[i] = 0.5f;
		}
	}
	
	public float Adjust(float val, int index)
	{
		if (MinRot[index] > MaxRot[index])
		{//角度が逆なら入れ替えておく
			float temp = MinRot[index];
			MinRot[index] = MaxRot[index];
			MaxRot[index] = temp;
		}
		if (MaxRot[index] < val)
		{
			if (Mirror[index])
				return MaxRot[index] * (1 + Restitution[index]) - val * Restitution[index];
			else
				return MaxRot[index];
		}
		else if (MinRot[index] > val)
		{
			if (Mirror[index])
				return MinRot[index] * (1 + Restitution[index]) - val * Restitution[index];
			else
				return MinRot[index];
			}
		else
			return val;
	}
}

public class DefaultIKLimitter
{
	Dictionary<string, RotationLimit> TotalRotationLimits;
	
	public DefaultIKLimitter()
	{	// 制御制限
		TotalRotationLimits = new Dictionary<string, RotationLimit>();
		RotationLimit limit;
		limit = new RotationLimit();
		limit.MaxRot[0] = Mathf.PI;
		limit.MinRot[0] = Mathf.Deg2Rad *3.0f;
		limit.MinRot[1] = 0;
		limit.MaxRot[1] = 0;
		limit.MinRot[2] = 0;
		limit.MaxRot[2] = 0;
		limit.Mirror[0] = true;
		limit.Restitution[0] = 0.99f;
		TotalRotationLimits.Add("左ひざ", limit);
		limit = new RotationLimit();
		limit.MaxRot[0] = Mathf.PI;
		limit.MinRot[0] = Mathf.Deg2Rad * 3.0f;
		limit.MinRot[1] = 0;
		limit.MaxRot[1] = 0;
		limit.MinRot[2] = 0;
		limit.MaxRot[2] = 0;
		limit.Mirror[0] = true;
		limit.Restitution[0] = 0.99f;
		TotalRotationLimits.Add("右ひざ", limit);
		
		//IKのソルブ及びそれの調整計算に関するメモ 
		//上記数値調整計算及び各種数値設定はMMDの元コード推定(リバースエンジニアリング、逆コンパイラとかはしてないからRエンジニアって言うのか分からないけど)する過程で落ち着いている今のところの解です。 
		//ほんとの解法は樋口さんが知ってるんだろうけどｗ 
		//解法は今のところIK-CCD法がMMDにとって最適だと考えてます。 
		//理由として 
		//・ひざのボーンにIKソルブ時の角度制限が入っているっぽいので、ソルブにボーンの角度を扱う必要があること 
		//・高速解法が必要であること(MMDが非常に軽いことと、イテレーションの存在とその回数を考えると、軽いアルゴリズムを使ってないとつじつまが合わない) 
		//が上げられます 
		//そこで、CCD,Particleかの二つで、角度を使い易かったCCDを選びました。 
		//ひざの角度調整はCCDのクセを抑える理由もあって工夫してあります。 
		//CCDのクセとして、正しい解が＜だとしたら、＞という解を出してくることが多いという問題があります。(＞＜は足ですｗ) 
		//そのために"反発係数"なる謎なパラメータを付けてますｗ 
		//また、解がほとんどまっすぐな解を出す際に、|な感じの解で固定されてしまう問題があるため、3度ぐらい下限を入れています(どうも、MMDの方も入れてるっぽいけど、よく分からない……) 
		//これは現在の推定結果です。もっと再現性が高い解があれば、改造して、ぜひ教えてください 
	}
	
	public void Adjust(GameObject bone)
	{
		if (!TotalRotationLimits.ContainsKey(bone.name))
			return;
		float[] degrot = new float[3];
		int FactoringType = 0;
		//if (MMDMathf.FactoringQuaternionZXY(rot, out degrot[2], out degrot[0], out degrot[1]))
		//まずはXYZで分解
		if (!FactoringQuaternionXYZ(bone.transform.localRotation, degrot))
		{//ジンバルロック対策
			//YZXで分解
			if (!FactoringQuaternionYZX(bone.transform.localRotation, degrot))
			{
				//ZXYで分解
				FactoringQuaternionZXY(bone.transform.localRotation, degrot);
				FactoringType = 2;
			}
			else
				FactoringType = 1;
		}
		else
			FactoringType = 0;

		RotationLimit lim = TotalRotationLimits[bone.name];
		degrot[0] = lim.Adjust(degrot[0], 0);
		degrot[1] = lim.Adjust(degrot[1], 1);
		degrot[2] = lim.Adjust(degrot[2], 2);
		if (FactoringType == 0)
			bone.transform.localRotation = MMDMathf.CreateQuaternionFromRotationMatrix(
					MMDMathf.CreateRotationXMatrix(degrot[0]) *
					MMDMathf.CreateRotationYMatrix(degrot[1]) *
					MMDMathf.CreateRotationZMatrix(degrot[2]));
		else if (FactoringType == 1)
			bone.transform.localRotation = MMDMathf.CreateQuaternionFromRotationMatrix(
					MMDMathf.CreateRotationYMatrix(degrot[1]) *
					MMDMathf.CreateRotationZMatrix(degrot[2]) *
					MMDMathf.CreateRotationXMatrix(degrot[0]));
		else
			bone.transform.localRotation = 
				MMDMathf.CreateQuaternionFromRotationMatrix(
					MMDMathf.CreateRotationMatrixFromRollPitchYaw(degrot[1], degrot[0], degrot[2]));
	}
	
	// クォータニオンから行列の生成 
	Matrix4x4 CreateMatrixFromQuaternion(Quaternion q)
	{
		Matrix4x4 m = Matrix4x4.identity;
		float x, y, z, w, sin;
		sin = Mathf.Sin(q.w * 0.5f);
		x = q.x * sin;
		y = q.y * sin;
		z = q.z * sin;
		w = Mathf.Cos(q.w * 0.5f);
		m.m00 = 1-2*y*y-2*z*z;
		m.m01 = 2*x*y+2*w*z;
		m.m02 = 2*x*z-2*w*y;
		m.m10 = 2*x*y-2*w*z;
		m.m11 = 1-2*x*x-2*z*z;
		m.m12 = 2*y*z+2*w*x;
		m.m20 = 2*x*z+2*w*y;
		m.m21 = 2*y*z-2*w*x;
		m.m22 = 1-2*x*x-2*y*y;
		return m;
	}
	
	Quaternion NormalizeQuaternion(Quaternion input)
	{
		float l = 1.0f / Mathf.Sqrt(input.x * input.x + input.y * input.y + input.z * input.z + input.w * input.w);
		input.x *= l;
		input.y *= l;
		input.z *= l;
		input.w *= l;
		return input;
	}
	
	// クォータニオンをYaw(Y回転), Pitch(X回転), Roll(Z回転)に分解する関数
	bool FactoringQuaternionZXY(Quaternion input, float[] degrot)
	{
		//クォータニオンの正規化
		Quaternion inputQ = new Quaternion(input.x, input.y, input.z, input.w);
		inputQ = NormalizeQuaternion(inputQ);
		
		//マトリクスを生成する
		Matrix4x4 rot = CreateMatrixFromQuaternion(inputQ);
		
		//ヨー(X軸周りの回転)を取得
		if (rot.m32 > 1 - 1.0e-4 || rot.m32 < -1 + 1.0e-4)
		{//ジンバルロック判定
			degrot[0] = (rot.m32 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[2] = 0; degrot[1] = (float)Mathf.Atan2(-rot.m13, rot.m11);
			return false;
		}
		degrot[0] = -(float)Mathf.Asin(rot.m32);
		//ロールを取得
		degrot[2] = (float)Mathf.Asin(rot.m12 / Mathf.Cos(degrot[0]));
		if (float.IsNaN(degrot[2]))
		{//漏れ対策
			degrot[0] = (rot.m32 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[2] = 0; degrot[1] = (float)Mathf.Atan2(-rot.m13, rot.m11);
			return false;
		}
		if (rot.m22 < 0)
			degrot[2] = Mathf.PI - degrot[2];
		//ピッチを取得
		degrot[1] = (float)Mathf.Atan2(rot.m31, rot.m33);
		return true;
	}
	
	// クォータニオンをX,Y,Z回転に分解する関数
	bool FactoringQuaternionXYZ(Quaternion input, float[] degrot)
	{
		//クォータニオンの正規化
		Quaternion inputQ = new Quaternion(input.x, input.y, input.z, input.w);
		inputQ = NormalizeQuaternion(inputQ);
		//マトリクスを生成する
		Matrix4x4 rot = CreateMatrixFromQuaternion(inputQ);
		
		//Y軸回りの回転を取得
		if (rot.m13 > 1 - 1.0e-4 || rot.m13 < -1 + 1.0e-4)
		{//ジンバルロック判定
			degrot[0] = 0;
			degrot[1] = (rot.m13 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[2] = -(float)Mathf.Atan2(-rot.m21, rot.m22);
			return false;
		}
		degrot[1] = -(float)Mathf.Asin(rot.m13);
		//X軸回りの回転を取得
		degrot[0] = (float)Mathf.Asin(rot.m23 / Mathf.Cos(degrot[1]));
		if (float.IsNaN(degrot[0]))
		{//ジンバルロック判定(漏れ対策)
			degrot[0] = 0;
			degrot[1] = (rot.m13 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[2] = -(float)Mathf.Atan2(-rot.m21, rot.m22);
			return false;
		}
		if (rot.m33 < 0)
			degrot[0] = Mathf.PI - degrot[0];
		//Z軸回りの回転を取得
		degrot[2] = (float)Mathf.Atan2(rot.m12, rot.m11);
		return true;
	}
		
	// クォータニオンをY,Z,X回転に分解する関数
	bool FactoringQuaternionYZX(Quaternion input, float[] degrot)
	{
		//クォータニオンの正規化
		Quaternion inputQ = new Quaternion(input.x, input.y, input.z, input.w);
		inputQ = NormalizeQuaternion(inputQ);
		//マトリクスを生成する
		Matrix4x4 rot = CreateMatrixFromQuaternion(inputQ);
		//Z軸回りの回転を取得
		if (rot.m21 > 1 - 1.0e-4 || rot.m21 < -1 + 1.0e-4)
		{//ジンバルロック判定
			degrot[1] = 0;
			degrot[2] = (rot.m21 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[0] = -(float)Mathf.Atan2(-rot.m32, rot.m33);
			return false;
		}
		degrot[2] = -(float)Mathf.Asin(rot.m21);
		//Y軸回りの回転を取得
		degrot[1] = (float)Mathf.Asin(rot.m31 / Mathf.Cos(degrot[2]));
		if (float.IsNaN(degrot[1]))
		{//ジンバルロック判定(漏れ対策)
			degrot[1] = 0;
			degrot[2] = (rot.m21 < 0 ? Mathf.PI * 0.5f : -Mathf.PI * 0.5f);
			degrot[0] = -(float)Mathf.Atan2(-rot.m32, rot.m33);
			return false;
		}
		if (rot.m11 < 0)
			degrot[1] = Mathf.PI - degrot[1];
		//X軸回りの回転を取得
		degrot[0] = (float)Mathf.Atan2(rot.m23, rot.m22);
		return true;
	}
}
