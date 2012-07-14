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
    private ParticleSystem particle = null;

	void Start () 
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        counter = offset;
    }

	void FixedUpdate () 
    {
        if (valid) 
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

        // 終了エフェクト開始
        if (particle)
        {
            particle.Play();
            Debug.Log("Particle Start");
        }

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
        if (particle)
        {
            while (particle.isPlaying)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
        // 削除メッセージ
        Debug.Log("Destory :" + transform.parent.gameObject);
        transform.parent.gameObject.SendMessage("OnDestroyObject", SendMessageOptions.DontRequireReceiver);
    }
}
