using UnityEngine;
using System.Collections;

public class DamageLvText : MonoBehaviour {

    [SerializeField]
    private int disitSize = 1;

    /// <summary>
    /// [SendMessage]表示更新
    /// </summary>
    /// <param name="value"></param>
    void OnDisplayDamageLv(int value)
    {
        guiText.text = value.ToString("D" + disitSize);
    }
	
}
