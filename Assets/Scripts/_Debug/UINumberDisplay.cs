using UnityEngine;
using System.Collections;

public class UINumberDisplay : MonoBehaviour
{
    [SerializeField]
    private int disit = 3;
    [SerializeField]
    private Texture[] sources;

    private Texture[] texture;
    private Rect[] rect;

	void Start () 
    {
        rect = new Rect[disit];
        texture = new Texture[disit];
	}

    public void SetValue( int value )
    {
        for (int i = disit; i <= 0; i--)
        {
            ;
        }
    }
    void OnGUI()
    {
        for (int i = 0; i < disit; i++ )
        {
            GUI.DrawTexture(rect[i], texture[i]);
        }
    }
}
