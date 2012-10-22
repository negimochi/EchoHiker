using UnityEngine;
using System.Collections;

/// <summary>
/// ヒットエフェクト専用。
/// OnHitが伝わったときに、パーティクルとサウンドを再生する。
/// </summary>
public class HitEffector : MonoBehaviour {

//    [SerializeField]
    private bool valid = true;

    void Start()
    { 
    }

    // 無効なら事前に呼んでおく
    void OnInvalidEffect()
    {
        Debug.Log("HitEffector.OnInvalid");
        valid = false;
    }

    // ヒット時の挙動管理と終了タイミング
    void OnHit()
    {
        Debug.Log("HitEffector.OnHit");
        if (valid)
        {
            if (particleSystem) particleSystem.Play();
            if (audio) audio.Play();
        }
        else Debug.Log("HitEffector.OnHit: Invalid");
    }

    public bool IsFinished()
    {
        bool result = true;
        if (particleSystem) result = result && !particleSystem.isPlaying;
        if (audio) result = result && !audio.isPlaying;
        return result;
    }
    public bool IsPlaying()
    {
        bool result = false;
        if (particleSystem) result = result || particleSystem.isPlaying;
        if (audio) result = result || audio.isPlaying;
        return result;
    }
}
