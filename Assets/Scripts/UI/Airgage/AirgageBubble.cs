using UnityEngine;
using System.Collections;

/// <summary>
/// ��C�c�ʂ̖A���o�Ă�G�t�F�N�g
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
