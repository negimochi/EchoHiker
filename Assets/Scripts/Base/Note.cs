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
//    private float param;
    private Color baseColor;

    private ParticleSystem particleSystem = null;

    public void Valid(bool valid_) { valid = valid_; }

    void OnHit()
    {
        Debug.Log("OnHit"); 
        valid = false;
        // Stop�Ǝg���Ɖ����Ԃ؂�ɂȂ�ꍇ�����邽�߁A���ʂ��t�F�[�h�A�E�g�����đΉ�
        //audio.Stop();

        // �G�t�F�N�g�J�n(1�����Ƃ���)
        if (particleSystem) {
            particleSystem.Play();
            Debug.Log("Particle Start");
        }

        StartCoroutine("Fadeout", 1.0f);
    }

	void Start () 
    {
        particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
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
//            param = 1.0f;
            counter = 0.0f;
            //Debug.Log(name + ":Play");
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�R���[�`��
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Fadeout(float duration)
    {
        // �t�F�[�h�A�E�g
        float currentTime = 0.0f;
        float waitTime = 0.02f;
        float firstVol = audio.volume;
        while (duration > currentTime)
        {
            audio.volume = Mathf.Lerp(firstVol, 0.0f, currentTime / duration);
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }

        // �G�t�F�N�g�����S�ɏI�����Ă�����I�u�W�F�N�g�j��
        if (particleSystem)
        {
            while (particleSystem.isPlaying)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
        // �폜���b�Z�[�W
        Debug.Log("Destory :" + transform.parent.gameObject);
        transform.parent.gameObject.SendMessage("OnDestroyObject", SendMessageOptions.DontRequireReceiver);
    }
}
