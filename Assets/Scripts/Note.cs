using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private float interval = 1.0f;
    [SerializeField]
    private float offset = 0.0f;
    [SerializeField]
    private bool visible = true;
    [SerializeField]
    private bool valid   = true;

    private float counter;
    private float param;
    private Color baseColor;

    private void OnHitItem()
    {
        Debug.Log("OnHitItem"); 
        valid = false;
        // Stop�Ǝg���Ɖ����Ԃ؂�ɂȂ�ꍇ�����邽�߁A���ʂ��t�F�[�h�A�E�g�����đΉ�
        //audio.Stop();

        // �G�t�F�N�g�J�n(1�����Ƃ���)
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
//        Debug.Log("Emitter" + emitter);
        if (particleSystem) {
            particleSystem.Play();
            Debug.Log("Particle Start");
        }

        StartCoroutine("Fadeout", 1.0f);
        Debug.Log("OnHitItem End");
    }

    private IEnumerator Fadeout(float duration)
    {
        // �t�F�[�h�A�E�g
        float currentTime = 0.0f;
        float waitTime = 0.02f;
        float firstVol = audio.volume;
        while (duration > currentTime)
        {
//            audio.volume = Mathf.Clamp01(firstVol * (duration - currentTime) / duration);
            audio.volume = Mathf.Lerp( firstVol, 0.0f, currentTime/duration );
            Debug.Log("Step:" + audio.volume);
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }

        // �G�t�F�N�g�����S�ɏI�����Ă�����I�u�W�F�N�g�j��
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        while (particleSystem.isPlaying)
        {
            yield return new WaitForSeconds(waitTime);
        }
        Destroy( gameObject );
        Debug.Log("Destory");
    }

    private void Clock(float step)
    {
        if (valid)
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
    }

    // Use this for initialization
	void Start () 
    {
        counter = offset;
        renderer.enabled = visible;
//        baseColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b);
//        param = 1.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (valid)
        {
            Clock(Time.deltaTime);
//            if (visible)
//            {
//                param *= Mathf.Exp(-3.0f * Time.deltaTime);
//                //	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
//                Color color = new Color(Mathf.Abs(baseColor.r - param), baseColor.g, baseColor.b);
//                renderer.material.color = color;
//            }
        }
	}

}
