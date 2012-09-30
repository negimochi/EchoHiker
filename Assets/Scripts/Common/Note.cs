using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private float interval = 1.0f;
    [SerializeField]
    private float offset = 0.0f;
    [SerializeField]
    private bool valid   = true;

    private float counter = 0.0f;

    private HitEffector hitEffector = null;

	void Start () 
    {
        hitEffector = gameObject.GetComponentInChildren<HitEffector>();
        counter = offset;
    }

	void FixedUpdate () 
    {
        if (valid && !audio.isPlaying) 
        {
            Clock(Time.deltaTime);
        }
	}


    private void Clock(float step)
    {
        counter += step;
        if (counter >= interval)
        {
            audio.Play();
            counter = 0.0f;
        }
    }

    /// <summary>
    /// 音の有効・無効
    /// </summary>
    /// <param name="flag"></param>
    public void SetEnable(bool flag) { valid = flag; }

    void OnHit()
    {
        valid = false;
        // Stopと使うと音がぶつ切りになる場合があるため、音量をフェードアウトさせて対応
        //audio.Stop();
        StartCoroutine("Fadeout", 1.0f);
    }


    /// <summary>
    /// フェードアウトコルーチン
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Fadeout(float duration)
    {
        // フェードアウト
        float currentTime = 0.0f;
        float waitTime = 0.02f;
        float firstVol = audio.volume;
        while (duration > currentTime)
        {
            audio.volume = Mathf.Lerp(firstVol, 0.0f, currentTime / duration);
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }

        // エフェクトが完全に終了していたらオブジェクト破棄
        if (hitEffector)
        {
            while (hitEffector.IsPlaying())
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
        // 削除メッセージを要求
        transform.parent.gameObject.SendMessage("OnDestroyObject", SendMessageOptions.DontRequireReceiver);
    }
}
