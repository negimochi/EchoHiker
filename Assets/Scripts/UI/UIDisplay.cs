using UnityEngine;
using System.Collections;

public class UIDisplay : MonoBehaviour {

    [SerializeField]
    private GUIStyle style;
    [SerializeField]
    private float[] updateTime = new float[] {
        8.0f, 5.0f, 3.0f, 2.0f, 1.0f, 0.5f
    };
    [SerializeField]
    private int damageLevel = 0;


    private static string scoreText = "Score: ";
    private static string airText = "Air: ";
    private static string cautionText = "Caution: ";

    private int score = 0;
    private int air = 1000;
    private int caution = 0;

    private float counter = 0;
    private bool gameover = false;


	void Start () 
    {
	}

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > updateTime[damageLevel]) {
            OnDeflate(1);
            counter = 0;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.5f, 10.0f, 120.0f, 20.0f), scoreText + score/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, 10.0f, 120.0f, 20.0f), airText + air/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, 30.0f, 120.0f, 20.0f), "Damage Lv." + damageLevel/*, style*/);
        GUI.Label(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, 120.0f, 20.0f), cautionText + caution.ToString() + "%");

        if (gameover)
        {
            GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 120.0f, 20.0f), "Game Over");
        }
    }

    void OnGetItem( int value )
    {
        score += value;
    }

    void OnDestroyEnemy( int value )
    {
        score += value;
    }

    void OnDamage( int value )
    {
        damageLevel += value;
        if( damageLevel >= updateTime.Length ) damageLevel = updateTime.Length - 1;
    }

    void OnDeflate( int value )
    {
        air -= value;
        
        if (air <= 0) {
            air = 0;
            // I—¹’Ê’m‚ð‘—‚é
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) player.BroadcastMessage("OnGameOver");
            GameObject obj = GameObject.Find("/Object");
            if (obj) obj.BroadcastMessage("OnGameOver");
            gameover = true;
        }
    }

    void OnUpdateCaution( int value )
    {
        caution = value;
    }
}
