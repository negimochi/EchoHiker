using UnityEngine;
using System.Collections;

public class Airgage : MonoBehaviour {

    [SerializeField]
    private float[] airUpdateTime = new float[] {
        8.0f, 5.0f, 3.0f, 2.0f, 1.0f, 0.5f
    };

    [SerializeField]
    private int air = 1000;

    private int damageLevel = 0;

    void Start() 
    {
	    
	}

    void OnDamage(int value)
    {
        damageLevel += value;
        if (damageLevel >= airUpdateTime.Length) damageLevel = airUpdateTime.Length - 1;
    }

    void OnDeflate(int value)
    {
        air -= value;

        if (air <= 0)
        {
            air = 0;
            // I—¹’Ê’m‚ð‘—‚é
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) player.BroadcastMessage("OnGameOver");
            GameObject obj = GameObject.Find("/Object");
            if (obj) obj.BroadcastMessage("OnGameOver");
            //gameover = true;
        }
    }

    public int Value() { return air; }
    public int DamageLevel() { return damageLevel; }
}
