using UnityEngine;
using System.Collections;

public class HitEffector : MonoBehaviour {

    // ヒット時の挙動管理と終了タイミング
    void OnHit()
    {
        Debug.Log("HitEffector.OnHit");
        if (particleSystem) particleSystem.Play();
        if (audio) audio.Play();
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
