using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MMDEngine : MonoBehaviour {

	public bool useRigidbody = false;
	public int[] groupTarget;		// 非衝突剛体リスト
	public GameObject[] rigids;		// 剛体リスト

	// 訳があってこうなってる
	public int[] ignore1;
	public int[] ignore2;
	public int[] ignore3;
	public int[] ignore4;
	public int[] ignore5;
	public int[] ignore6;
	public int[] ignore7;
	public int[] ignore8;
	public int[] ignore9;
	public int[] ignore10;
	public int[] ignore11;
	public int[] ignore12;
	public int[] ignore13;
	public int[] ignore14;
	public int[] ignore15;
	public int[] ignore16;
	List<int[]> ignoreList;

	// Use this for initialization
	void Start () 
	{
		if (useRigidbody)
		{
			ignoreList = new List<int[]>();
			ignoreList.Add(ignore1);
			ignoreList.Add(ignore2);
			ignoreList.Add(ignore3);
			ignoreList.Add(ignore4);
			ignoreList.Add(ignore5);
			ignoreList.Add(ignore6);
			ignoreList.Add(ignore7);
			ignoreList.Add(ignore8);
			ignoreList.Add(ignore9);
			ignoreList.Add(ignore10);
			ignoreList.Add(ignore11);
			ignoreList.Add(ignore12);
			ignoreList.Add(ignore13);
			ignoreList.Add(ignore14);
			ignoreList.Add(ignore15);
			ignoreList.Add(ignore16);

			// 非衝突グループの設定
			for (int i = 0; i < rigids.Length; i++)
			{
				for (int shift = 0; shift < 16; shift++)
				{
					// フラグチェック
					if ((groupTarget[i] & (1 << shift)) == (1 << shift))
					{
						for (int j = 0; j < ignoreList[shift].Length; j++)
						{
							if (i == j) continue;
							Physics.IgnoreCollision(rigids[i].collider, rigids[j].collider, true);
						}
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public static void Initialize(MMDEngine engine, int[] groupTarget, List<int>[] ignoreGroups, GameObject[] rigidArray)
	{
		if (!engine.useRigidbody)
		{
			engine.groupTarget = groupTarget;
			engine.rigids = rigidArray;
			engine.useRigidbody = true;

			engine.ignore1 = ignoreGroups[0].ToArray();
			engine.ignore2 = ignoreGroups[1].ToArray();
			engine.ignore3 = ignoreGroups[2].ToArray();
			engine.ignore4 = ignoreGroups[3].ToArray();
			engine.ignore5 = ignoreGroups[4].ToArray();
			engine.ignore6 = ignoreGroups[5].ToArray();
			engine.ignore7 = ignoreGroups[6].ToArray();
			engine.ignore8 = ignoreGroups[7].ToArray();
			engine.ignore9 = ignoreGroups[8].ToArray();
			engine.ignore10 = ignoreGroups[9].ToArray();
			engine.ignore11 = ignoreGroups[10].ToArray();
			engine.ignore12 = ignoreGroups[11].ToArray();
			engine.ignore13 = ignoreGroups[12].ToArray();
			engine.ignore14 = ignoreGroups[13].ToArray();
			engine.ignore15 = ignoreGroups[14].ToArray();
			engine.ignore16 = ignoreGroups[15].ToArray();
		}
	}
}

