using UnityEngine;
using System.Collections;

public class DamageLvText : MonoBehaviour {

    [SerializeField]
    private int disitSize = 1;

    /// <summary>
    /// [SendMessage]�\���X�V
    /// </summary>
    /// <param name="value"></param>
    void OnDisplay(int value)
    {
        guiText.text = value.ToString("D" + disitSize);
    }
	
}
