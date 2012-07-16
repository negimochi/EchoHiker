using UnityEngine;
using System.Collections;

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
