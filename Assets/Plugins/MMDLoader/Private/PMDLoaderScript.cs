using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PMDLoaderScript {

	//--------------------------------------------------------------------------------
	// ファイル読み込み
	
	GameObject obj;
	public Object pmd;
	public bool cutoutFlag;		// Cutoutシェーダーを利用するか 
	public bool rigidFlag;		// 物理や剛体のオン・オフ
	
	BinaryReader LoadFile(Object obj, string path) {
		FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
		BinaryReader r = new BinaryReader(f);
		return r;
	}
		
	// PMDファイル読み込み
	void LoadPMDFile() {
		string path = AssetDatabase.GetAssetPath(this.pmd);
		BinaryReader bin = this.LoadFile(this.pmd, path);
		MMD.PMD.PMDFormat format = MMD.PMD.PMDLoader.Load(bin, obj, path);
		BurnUnityFormatForPMD(format);
		bin.Close();
	}
		
	// Use this for initialization
	public PMDLoaderScript (Object pmdFile, bool cutoutFlag, bool rigidFlag) {
		this.pmd = pmdFile;
		this.cutoutFlag = cutoutFlag;
		this.rigidFlag = rigidFlag;
		
		if (this.pmd != null) {
			LoadPMDFile();
		}
	}
	
	//--------------------------------------------------------------------------------
	// PMDファイルの読み込み
	
	Mesh mesh;
	Material[] materials;
	GameObject[] bones;
	GameObject[] rigids;
	
	void CreatePrefab(MMD.PMD.PMDFormat format)
	{
		Object prefab = EditorUtility.CreateEmptyPrefab(format.folder + "/" + format.name + ".prefab");
		EditorUtility.ReplacePrefab(format.caller, prefab);
	}
	
	void EndOfScript(MMD.PMD.PMDFormat format)
	{
		AssetDatabase.Refresh();
		
		this.mesh = null;
		this.materials = null;
		this.bones = null;
		this.pmd = null;
	}
	
	// PMDファイルをUnity形式に変換
	void BurnUnityFormatForPMD(MMD.PMD.PMDFormat format) {
		obj = new GameObject(format.name);
		format.caller = obj;
		
		MMD.PMD.PMDConverter conv = new MMD.PMD.PMDConverter();
		
		this.mesh = conv.CreateMesh(format);
		this.materials = conv.CreateMaterials(format, cutoutFlag);
		this.bones = conv.CreateBones(format);

		conv.BuildingBindpose(format, this.mesh, this.materials, this.bones);
		obj.AddComponent<Animation>();	// アニメーションを追加
		conv.EntryIKSolver(format, this.bones);

		MMDEngine engine = obj.AddComponent<MMDEngine>();

		// 剛体関連
		if (this.rigidFlag)
		{
			this.rigids = conv.CreateRigids(format, bones);
			conv.AssignRigidbodyToBone(format, this.bones, this.rigids);
			conv.SetRigidsSettings(format, this.bones, this.rigids);
			conv.SettingJointComponent(format, this.bones, this.rigids);
			
			// 非衝突グループ
			List<int>[] ignoreGroups = conv.SettingIgnoreRigidGroups(format, this.rigids);
			int[] groupTarget = conv.GetRigidbodyGroupTargets(format, rigids);
			
			MMDEngine.Initialize(engine, groupTarget, ignoreGroups, this.rigids);
		}
		
		CreatePrefab(format);
		EndOfScript(format);
	}
}
