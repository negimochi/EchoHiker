using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    [SerializeField]
    private float stepY = 5.0f;

    private GUITexture guiCompass;

	void Start () 
    {
        GameObject gameObj = GameObject.Find("/UI/Compass");
        if (gameObj) { 
            guiCompass = gameObj.GetComponent<GUITexture>();
        }
    }
	
	void Update () 
    {
        // �h���b�O�J�n
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseButtonDown");
        }
        // �h���b�O��
        if (Input.GetMouseButton(0) ) {
            Debug.Log("MouseButton");
            Rotate( Input.GetAxis("Mouse X") );
        }
        // �h���b�O�I��
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MouseButtonUp");
        }

        if (Input.touchCount > 0)
        {
//            if (guiTexture.HitTest())
            {

            }
        }

//        if( Input.GetAxis("Fire1") ) {
  //          ;
    //    }
	}

    void Rotate(float movement) 
    {
        transform.Rotate(0, movement * stepY, 0);
        if (guiCompass)
        {
            Debug.Log("guiCompass");
//            guiCompass..transform.Rotate(0, 0, movement * stepY);
        }
    }
}
