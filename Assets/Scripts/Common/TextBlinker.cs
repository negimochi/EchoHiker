using UnityEngine;
using System.Collections;

/// <summary>
/// �e�L�X�g�̓_��
/// </summary>
public class TextBlinker : MonoBehaviour
{
    [SerializeField]
    private bool valid = true;
    [SerializeField]
    private float blinkTime = 0.8f;
    [SerializeField]
    private int num = 5;

    private int count = 0;

    void Start()
    {
    }

    void OnStartTextBlink()
    {
        // Blink���X�^�[�g�����郁�b�Z�[�W
        if (!guiText || !valid) return;
        count = 0;
        guiText.enabled = true;
        StartCoroutine("Delay", blinkTime);
    }


    private IEnumerator Delay(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        guiText.enabled = !guiText.enabled;
        count++;
        if (count < num)
        {
            StartCoroutine("Delay", blinkTime);
        }
        else
        {
            SendMessage("OnEndTextBlink", SendMessageOptions.DontRequireReceiver);
        }
    }
}
