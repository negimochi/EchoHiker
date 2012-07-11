using UnityEngine;
using System.Collections;

public class Caution : MonoBehaviour {

    [SerializeField]
    private int offsetPixel = 8;
    [SerializeField]
    private int disitSize = 3;
    [SerializeField]
    private Color textColor = Color.yellow;

    private int cautionValue = 0;

	void Start () 
    {
        GUITexture texture = GetComponentInChildren<GUITexture>();
        if (texture)
        {
            float xPosPixel = Screen.width - texture.texture.width - offsetPixel;
            float yPosPixel = offsetPixel;
            gameObject.transform.position = new Vector3( xPosPixel/(float)Screen.width, yPosPixel/(float)Screen.height, 0.0f);
        }

        guiText.material.color = textColor;
        GUIText[] textArr = GetComponentsInChildren<GUIText>();
        for (int i = 0; i < textArr.Length; i++ )
        {
            textArr[i].material.color = textColor;
        }
    }

    void OnUpdateCaution(int value)
    {
        cautionValue = value;
        if (cautionValue > 100) cautionValue = 100;
        // •\Ž¦
        guiText.text = cautionValue.ToString("D" + disitSize);
    }

    public int Value() { return cautionValue; }

}
