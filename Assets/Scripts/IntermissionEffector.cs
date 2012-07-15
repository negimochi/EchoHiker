using UnityEngine;
using System.Collections;

public class IntermissionEffector : MonoBehaviour {

//    [System.Serializable]
    public enum Type
    {
        SlideIn,
        SlideOut
    };
    [SerializeField]
    private Type type = Type.SlideIn;
    [SerializeField]
    private float slideTime = 1.0f;
    [SerializeField]
    private int fadeAreaPixel = 200;
    [SerializeField]
    private bool playOnAwake = false;

    private float currentTime = 0.0f;
    private bool slide = false;

    private float startPos = 0.0f;
    private float endPos = 0.0f;
    private GameObject uiObj = null;

	void Start () 
    {
        uiObj = GameObject.Find("/UI");
        float openPos = (float)guiTexture.texture.height;
        float closePos = -(float)fadeAreaPixel;
        switch (type) {
            case Type.SlideIn:
                startPos = openPos;
                endPos = closePos;
                break;
            case Type.SlideOut: 
                startPos = closePos;
                endPos = openPos;
                break;
            default:break;
        }
        guiTexture.pixelInset = new Rect(0, startPos, (float)Screen.width, startPos);

        if (playOnAwake) StartIntermission();
	}
	
	void Update () 
    {
        if (slide)
        {
            currentTime += Time.deltaTime;
            float timeRate = currentTime/slideTime;
            float newPosY = Mathf.Lerp(startPos, endPos, timeRate);
            guiTexture.pixelInset = new Rect(0, newPosY, (float)Screen.width, startPos);
            if (timeRate >= 1.0f)
            {
                slide = false;
                uiObj.SendMessage("OnIntermissionEnd");
            }
        }
	}
    
    void StartIntermission()
    {
        slide = true;
        currentTime = 0.0f;
    }

    void OnIntermissionStart()
    {
        StartIntermission();
    }

}
