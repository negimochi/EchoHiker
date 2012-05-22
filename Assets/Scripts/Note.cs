using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
    [SerializeField]
    private float interval;
    [SerializeField]
    private float offset;
    private float counter;
    [SerializeField]
    private bool visible = true;
    [SerializeField]
    private bool valid = true;

    private float param;
    private Color baseColor;

    void OnHitItem()
    {
        Debug.Log("OnHitItem"); 
        valid = false;
        // Stop�Ǝg���Ɖ����Ԃ؂�ɂȂ�ꍇ�����邽�߁A�����ł͉��ʂ��t�F�[�h�A�E�g�����đΉ�
        //audio.Stop();

        // �G�t�F�N�g�J�n(1�����Ƃ���)
        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
//        Debug.Log("Emitter" + emitter);
        if (particleSystem) {
            particleSystem.Play();
            Debug.Log("Particle Start");
        }

        StartCoroutine("Fadeout", 1.0f);
    }

    IEnumerator Fadeout( float endTime )
    {
        // �t�F�[�h�A�E�g
        float step = 0.0f;
        float interval = 0.1f;
        float vol = audio.volume;
        while (endTime > step ) {
            audio.volume = vol * (1.0f - step / endTime);
            step += interval;
            Debug.Log("Step:" + step);
            yield return new WaitForSeconds(interval);
        }

        // �t�F�[�h�A�E�g��A�p�[�e�B�N���̎��Ԃ����҂��Ă���I�u�W�F�N�g�j��
        float delay = (particleSystem) ? particleSystem.duration : 0;
        Debug.Log("Destory:delay=" + delay);
        Destroy(gameObject, delay );
    }

    void OnClock( int delay )
    {
        if (valid)
        {
            counter ++;
            if (counter >= interval)
            {
               // AudioSource audio = GetComponent<AudioSource>();
                audio.Play((ulong)delay);
                param = 1.0f;
                counter = 0;
                Debug.Log(name + ":Play");
            }
        }
    }

    // Use this for initialization
	void Start () 
    {
        counter = offset;
        renderer.enabled = visible;
        baseColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b);
        param = 1.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (valid)
        {
            OnClock(0);
            if (visible)
            {
                param *= Mathf.Exp(-3.0f * Time.deltaTime);
                //	        transform.localScale = Vector3.one * (1.0f + param * 0.5f);
                Color color = new Color(Mathf.Abs(baseColor.r - param), baseColor.g, baseColor.b);
                renderer.material.color = color;
            }
        }
	}

}
