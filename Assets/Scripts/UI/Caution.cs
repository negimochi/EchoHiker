using UnityEngine;
using System.Collections;

public class Caution : MonoBehaviour {

    [SerializeField]
    private int offsetPixel = 8;

    private int cautionValue = 0;

	void Start () 
    {
        GUITexture texture = GetComponentInChildren<GUITexture>();
        if (texture)
        {
            int w = texture.texture.width;
            int h = texture.texture.height;
//            texture.pixelInset = new Rect(Screen.width - w - offsetPixel, offsetPixel, w, h);
            texture.pixelInset = new Rect(0, 0, w, h);
        }
	}

    void OnUpdateCaution(int value)
    {
        cautionValue = value;
    }

    public int Value() { return cautionValue; }

}
