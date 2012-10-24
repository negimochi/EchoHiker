using UnityEngine;
using System.Collections;

/// <summary>
/// ���[�U�N���b�N��AAdapter�ɃV�[���I����`����
/// </summary>
public class TitleSwitcher : MonoBehaviour {

    [SerializeField]
    private float waitTime = 3.0f;

    private bool pushed = false;
    private bool fade = false;
   
    void Start()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
    }

    void Update()
    {
        if (!guiText.enabled) return;

        if ( !pushed && Input.GetMouseButtonDown(0))
        {
            pushed = true;
            audio.Play();
            // �V�[���I����`����
            GameObject adapter = GameObject.Find("/Adapter");
            if (adapter) adapter.SendMessage("OnSceneEnd");
            else Debug.Log("adapter is not exist...");
        }
	}

    // �t�F�[�h�I�����ɌĂ΂��
    void OnEndTextFade()
    {
        if (!guiText.enabled) return;
        StartCoroutine("Delay");
    }

    // �X�C�b�`�̃X�^�[�g
    void OnStartSwitcher()
    {
        Debug.Log("OnStartSwitcher");
        guiText.enabled = true;
        fade = true;
        SendMessage("OnTextFadeIn");
    }
    // �X�e�[�W���Z�b�g
    void OnStageReset()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
        pushed = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        // FadeIn��FadeOut��؂�ւ��Ď��s
        fade = !fade;
        if (fade) SendMessage("OnTextFadeIn");
        else SendMessage("OnTextFadeOut");
    }

}
