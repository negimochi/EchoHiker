using UnityEngine;
using System.Collections;

public class Caution : MonoBehaviour {

    [SerializeField]
    private Vector2 offsetPixel = Vector2.zero;  // É[ÉçÇ≈âÊñ í[
    [SerializeField]
    private int disitSize = 3;
//    [SerializeField]
//    private Color safetyColor = Color.white;
    [SerializeField]
    private Color cautionColor = Color.yellow;
//    [SerializeField]
//    private Color emergencyColor = Color.red;

    private int cautionValue = 0;

	void Start () 
    {
        GUITexture texture = GetComponentInChildren<GUITexture>();
        if (texture)
        {
            // à íuí≤êÆ
            float w = (float)Screen.width;
            float h = (float)Screen.height;
            float xPos = 1.0f - (texture.texture.width + offsetPixel.x)/w;
            float yPos = offsetPixel.y/h;
            transform.position = new Vector3(xPos, yPos, 0.0f);
        }

        guiText.material.color = cautionColor;
        GUIText[] textArr = GetComponentsInChildren<GUIText>();
        for (int i = 0; i < textArr.Length; i++ )
        {
            textArr[i].material.color = cautionColor;
        }
    }

    void OnUpdateCaution(int value)
    {
        cautionValue = value;
        //if( cautionValue == 0 ) guiText.material.color = safetyColor;
        //else if (cautionValue < 90) guiText.material.color = cautionColor;
        //else  guiText.material.color = emergencyColor;
        // ï\é¶
        guiText.text = cautionValue.ToString("D" + disitSize);
    }

    public int Value() { return cautionValue; }

}
