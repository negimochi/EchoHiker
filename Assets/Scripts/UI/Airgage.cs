using UnityEngine;
using System.Collections;

public class Airgage : MonoBehaviour {

    [SerializeField]
    private float offsetGageSize = 120.0f;  // �[���ŉ�ʒ[
    [SerializeField]
    private Vector2 offsetPixelGage = Vector2.zero;  // �[���ŉ�ʒ[
    [SerializeField]
    private Vector2 offsetPixelText = Vector2.zero;  // �[���ŉ�ʒ[
    
    [SerializeField]
    private float[] airUpdateTime = new float[] {
        8.0f, 5.0f, 3.0f, 2.0f, 1.0f, 0.5f
    };  // �_�f������X�V�p�x

    [SerializeField]
    private float airMax = 1000.0f;     // air�̍ő�l
    [SerializeField]
    private float step = 1.0f;          // ��x�̍X�V�Ɍ����

    private float air = 0;              // ���݂�air�l

    private int damageLv = 0;           // �_���[�W���x��
    private float counter = 0;

    private GameObject meterObj;
    private GameObject damageLvObj;

    private GameObject uiObj = null;

    void Start()
    {
        air = airMax;
        meterObj = GameObject.Find("AirgageMeter");
        damageLvObj = GameObject.Find("DamageLvText");
        uiObj = GameObject.Find("/UI");

        // �ʒu����
        float w = (float)Screen.width;
        float h = (float)Screen.height;

        float aspect = w / h;
        offsetPixelGage.x += offsetGageSize;
        offsetPixelGage.y += offsetGageSize;
        float posX = aspect * (1.0f - offsetPixelGage.x / w);
        float posY = 1.0f - offsetPixelGage.y / h;
        meterObj.transform.position = new Vector3(posX, posY, 0.0f);

        posX = 1.0f - offsetPixelText.x / w;
        posY = 1.0f - offsetPixelText.y / h;
        damageLvObj.transform.position = new Vector3(posX, posY, 0.0f);

    }

    void Update()
    {
        // �J�E���^�ɂ��X�V
        counter += Time.deltaTime;
        if (counter > airUpdateTime[damageLv])
        {
            Deflate();
            counter = 0;
        }
    }

    /// <summary>
    /// [BroadcastMessage]
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="value">�_���[�W�ʁB�ʏ�1</param>
    void OnDamage(int value)
    {
        // �_���[�W���x�����Z
        damageLv += value;
        if (damageLv >= airUpdateTime.Length) damageLv = airUpdateTime.Length - 1;
        // �\���p�̃I�u�W�F�N�g�ɓ`����
        //damageLvObj.SendMessage("OnDisplayDamageLv", damageLv);
        BroadcastMessage("OnDisplayDamageLv", damageLv, SendMessageOptions.DontRequireReceiver);
    }

    void OnInflate(int value )
    {
        air += value;
    }

    /// <summary>
    /// air�X�V
    /// </summary>
    private void Deflate()
    {
        bool gameover = false;
        // �l�X�V
        air -= step;
        if( air <= 0.0f ) {
            air = 0.0f;
            gameover = true;
        }
        // ���[�^�[�ɒl��`����
        float threshold = Mathf.InverseLerp(0, airMax, air);
        meterObj.SendMessage("OnDisplayAirgage", threshold);

        if (gameover)
        {
            // �_�f�؂�B�Q�[���I�[�o�[
            uiObj.SendMessage("OnNotifyGameEnd");
        }
    }

    public float Air() { return air; }
    public int DamageLevel() { return damageLv; }
}
