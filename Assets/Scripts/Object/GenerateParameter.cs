using UnityEngine;
using System.Collections;

[System.Serializable]
public class GenerateParameter 
{
//    public float startTime = 0.0f;
    public int limitNum = 1;
    public float delayTime = 1.0f;
    public bool endless = true;   // リミット数から減った時に自動追加するか
}
