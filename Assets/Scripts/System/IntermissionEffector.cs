using UnityEngine;
using System.Collections;

public class IntermissionEffector : MonoBehaviour {

//    [System.Serializable]
    public enum Type
    {
        None,
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

    private float height = 0.0f;
    private float width = 0.0f;

    private float startPos = 0.0f;
    private float endPos = 0.0f;

    GameObject root = null;

	void Start () 
    {
        width  = (float)Screen.width;
        height = (float)guiTexture.texture.height;

        root = GameObject.Find("/Root");

        SetType(type);
        guiTexture.pixelInset = new Rect(0, endPos, width, height);

        if (playOnAwake) StartIntermission();
	}
	
	void Update () 
    {
        if (slide)
        {
            currentTime += Time.deltaTime;
            float timeRate = currentTime/slideTime;
            float newPosY = Mathf.Lerp(startPos, endPos, timeRate);
            guiTexture.pixelInset = new Rect(0, newPosY, width, height);
            if (timeRate >= 1.0f)
            {
                slide = false;
                //GameObject uiObj = GameObject.Find("/UI");
                //if (uiObj) uiObj.SendMessage("OnIntermissionEnd");
                //else Debug.Log("OnIntermissionEnd");
                if (root) root.SendMessage("OnIntermissionEnd");
            }
        }
	}
    
    private void StartIntermission()
    {
        Debug.Log("StartIntermission:" + type);
        slide = true;
        currentTime = 0.0f;
    }

    void OnIntermissionStart( Type type_ = Type.None )
    {
        Debug.Log("OnIntermissionStart:" + type_ + "/" + type);
        if (type_ == type)
        {
            if (root) root.SendMessage("OnIntermissionEnd");
            return;
        }
        if (type_ != Type.None) SetType(type_);
        StartIntermission();
    }

    void SetType(Type type_)
    { 
        type = type_;
        float closePos = -(float)fadeAreaPixel;
        switch (type)
        {
            case Type.SlideIn:
                startPos = height;
                endPos = closePos;
                break;
            case Type.SlideOut: 
                startPos = closePos;
                endPos = height;
                break;
            default:    break;
        }
    }
}
