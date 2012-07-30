using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;

    [SerializeField]
    private bool sonarHit = false;
    [SerializeField]
    private bool sonarInside = false;
   
    private float max;
    private float currentTime;
    private Color startColor;
    private bool wait;

	void Start () 
    {
        wait = false;
        max = 1.0f - minAlpha;
        currentTime = 0.0f;
        startColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a);

        // �������ꂽ�i�K�Ń\�i�[���ɂ��邩�`�F�b�N
        GameObject sonarCameraObj = GameObject.Find("/Player/SonarCamera");
        if (sonarCameraObj) {
            sonarCameraObj.SendMessage("OnInstantiatedChild", gameObject);
        }
	}

	void Update () 
    {
        if (!wait)
        {
            float time = currentTime / duration;
            if (time <= (2.0f*max))
            {
                float alpha = 1.0f - Mathf.PingPong(time, max);
                renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // ���ԍX�V
                currentTime += Time.deltaTime;
            }
            else
            {
                wait = true;
                StartCoroutine("Delay", delay);
            }
        }
	}

    void OnHit()
    {
        // �q�b�g�����u�ԂŃ\�i�[���猩���Ȃ�����
        Debug.Log("OnHit" + gameObject.transform.parent.gameObject.name);
        sonarHit = false;
        Enable();
    }

    void OnSonar()
    {
        // �\�i�[���猩���邱�Ƃ�������
        Debug.Log("OnSonar" + gameObject.transform.parent.gameObject.name);
        sonarHit = true;
        Enable();
    }

    void OnSonarInside()
    {
        // �\�i�[�\���̈�̓���
        Debug.Log("SonarInside:" + gameObject.transform.parent.gameObject.name);
        sonarInside = true;
        Enable();
    }

    void OnSonarOutside()
    {
        // �\�i�[�\���̈�̊O��
        Debug.Log("SonarOutside:" + gameObject.transform.parent.gameObject.name);
        sonarInside = false;
        Enable();
    }

    private void Enable()
    {
        renderer.enabled = sonarInside & sonarHit;
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
        currentTime = 0.0f;
    }

}
