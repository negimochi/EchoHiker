using UnityEngine;
using System.Collections;

public class MarineSnow : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 30.0f;

    
    //void OnGameOver() 
    //{
        //particleSystem.Pause();
    //}

    public void SetSpeed( float rate ) 
    {
        particleSystem.startSpeed = Mathf.Lerp(1.0f, maxSpeed, rate);
    }
}
