using UnityEngine;
using System.Collections;

[System.Serializable]
public class GenerateParameter 
{
    public Rect pos = new Rect(-900.0f, -900.0f, 1800.0f, 1800.0f);
    public bool fill = false;   // true: posXZ内を全部対象とする 
                                // flase: posXZの外周上を対象とする
    public int limitNum = 1;
    public float delayTime = 1.0f;
    public bool endless = true;   // リミット数から減った時に自動追加するか

}
