using UnityEngine;
using System.Collections;

/// <summary>
/// 空気残量の泡が出てるエフェクト
/// </summary>
public class AirgageBubble : MonoBehaviour {

    void OnDisplayDamageLv(int value)
    {
        particleSystem.emissionRate = 5 + 10 * (float)(value);
    }

    void OnGameOver()
    {
        particleSystem.Stop();
    }
}
