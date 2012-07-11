using UnityEngine;
using System.Collections;

public class Airgage : MonoBehaviour {

    
    [SerializeField]
    private float[] airUpdateTime = new float[] {
        8.0f, 5.0f, 3.0f, 2.0f, 1.0f, 0.5f
    };  // 酸素が減る更新頻度

    [SerializeField]
    private float airMax = 1000.0f;     // airの最大値
    [SerializeField]
    private float step = 1.0f;          // 一度の更新に減る量

    private float air = 0;              // 現在のair値

    private int damageLevel = 0;    // ダメージレベル
    private float counter = 0;

    void Start() 
    {
        air = airMax;
	}

    void Update()
    {
        // カウンタによる更新
        counter += Time.deltaTime;
        if (counter > airUpdateTime[damageLevel])
        {
            OnDeflate(step);
            counter = 0;
        }
    }

    void OnDamage(int value)
    {
        // ダメージレベル加算
        damageLevel += value;
        if (damageLevel >= airUpdateTime.Length) damageLevel = airUpdateTime.Length - 1;
    }

    void OnDeflate(float value)
    {
        // 値更新
        air -= value;
        // シェーダのアルファcutoffの値を変更して表示更新
        float threshold = Mathf.InverseLerp(0, airMax, air);
        Debug.Log( threshold );
        renderer.material.SetFloat("_Cutoff", threshold); 

        if (air <= 0)
        {
            air = 0.0f;

            // 酸素切れ。ゲームオーバー
            // 終了通知を送る
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
