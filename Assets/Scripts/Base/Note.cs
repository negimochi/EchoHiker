using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private float interval = 1.0f;
    [SerializeField]
    private float offset = 0.0f;
    [SerializeField]
    private bool valid   = true;

    private float counter;
    private float param;
    private Color baseColor;

    private void OnHit()
    {
        Debug.Log("OnHit"); 
        valid = false;
        // Stopと使うと音がぶつ切りになる場合があるため、音量をフェードアウトさせて対応
        //audio.Stop();

        // エフェクト開始(1つだけとする)
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
//        Debug.Log("Emitter" + emitter);
        if (particleSystem) {
            particleSystem.Play();
            Debug.Log("Particle Start");
        }

        StartCoroutine("Fadeout", 1.0f);
    }


    // Use this for initialization
	void Start () 
    {
        counter = offset;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (valid) {
            Clock(Time.deltaTime);
        }
	}


    private void Clock(float step)
    {
        counter += step;
        if (counter >= interval)
        {
            audio.Play();
            param = 1.0f;
            counter = 0.0f;
            Debug.Log(name + ":Play");
        }
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
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        while (particleSystem.isPlaying)
        {
            yield return new WaitForSeconds(waitTime);
        }
        // 削除メッセージ
        Debug.Log("Destory :" + transform.parent.gameObject);
        transform.parent.gameObject.SendMessage("OnDestroy", SendMessageOptions.DontRequireReceiver);
    }
}
