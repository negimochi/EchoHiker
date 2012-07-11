using UnityEngine;
using System.Collections;

public class Airgage : MonoBehaviour {

    
    [SerializeField]
    private float[] airUpdateTime = new float[] {
        8.0f, 5.0f, 3.0f, 2.0f, 1.0f, 0.5f
    };  // �_�f������X�V�p�x

    [SerializeField]
    private float airMax = 1000.0f;     // air�̍ő�l
    [SerializeField]
    private float step = 1.0f;          // ��x�̍X�V�Ɍ����

    private float air = 0;              // ���݂�air�l

    private int damageLevel = 0;    // �_���[�W���x��
    private float counter = 0;

    void Start() 
    {
        air = airMax;
	}

    void Update()
    {
        // �J�E���^�ɂ��X�V
        counter += Time.deltaTime;
        if (counter > airUpdateTime[damageLevel])
        {
            OnDeflate(step);
            counter = 0;
        }
    }

    void OnDamage(int value)
    {
        // �_���[�W���x�����Z
        damageLevel += value;
        if (damageLevel >= airUpdateTime.Length) damageLevel = airUpdateTime.Length - 1;
    }

    void OnDeflate(float value)
    {
        // �l�X�V
        air -= value;
        // �V�F�[�_�̃A���t�@cutoff�̒l��ύX���ĕ\���X�V
        float threshold = Mathf.InverseLerp(0, airMax, air);
        Debug.Log( threshold );
        renderer.material.SetFloat("_Cutoff", threshold); 

        if (air <= 0)
        {
            air = 0.0f;

            // �_�f�؂�B�Q�[���I�[�o�[
            // �I���ʒm�𑗂�
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) player.BroadcastMessage("OnGameOver");
            GameObject obj = GameObject.Find("/Object");
            if (obj) obj.BroadcastMessage("OnGameOver");
            //gameover = true;
        }
    }

    public float Value() { return air; }
    public int DamageLevel() { return damageLevel; }
}
