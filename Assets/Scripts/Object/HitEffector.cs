using UnityEngine;
using System.Collections;

public class HitEffector : MonoBehaviour {

    // �q�b�g���̋����Ǘ��ƏI���^�C�~���O
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
