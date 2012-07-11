using UnityEngine;
using System.Collections;

public class Caution : MonoBehaviour {

    [SerializeField]
    private int offsetPixel = 8;
    [SerializeField]
    private int disitSize = 3;

    private int cautionValue = 0;

	void Start () 
    {
        GUITexture texture = GetComponentInChildren<GUITexture>();
        if (texture)
        {
            int w = texture.texture.width;
            int h = texture.texture.height;
            Debug.Log(w + "," + h);
            texture.pixelInset = new Rect(Screen.width - w - offsetPixel, offsetPixel, w, h);

            guiText.pixelOffset = new Vector2(texture.pixelInset.x + 32, texture.pixelInset.y + 42);
            guiText.material.color = Color.yellow;
        }
        GUIText text = GetComponentInChildren<GUIText>();
        if (text) {
            text.pixelOffset = new Vector2(guiText.pixelOffset.x, guiText.pixelOffset.y);
            text.material.color = Color.yellow;
        }
    }

    void OnUpdateCaution(int value)
    {
        cautionValue = value;
        guiText.text = cautionValue.ToString("D" + disitSize);
    }

    public int Value() { return cautionValue; }

}
