using UnityEngine;
using System.Collections;

public class UIDebugDisplay : MonoBehaviour {

    private Hashtable itemCounter;

	private float oldTime;
	private int frame = 0;
	private float frameRate = 0.0f;
	private const float interval = 0.5f;

	void Start () 
    {
        itemCounter = new Hashtable();
		oldTime = Time.realtimeSinceStartup;
	}
 
	private void Update()
	{
		frame++;
		float time = Time.realtimeSinceStartup - oldTime;
		if (time >= interval) {
			frameRate = frame / time;
			oldTime = Time.realtimeSinceStartup;
			frame = 0;
		}
	}
    /*
    void OnGetItem(GameObject histObj)
    {
        string objname = histObj.name;
        if (!itemCounter.Contains(objname))
        {
            itemCounter.Add(objname, 1);
        }
        else
        {
            itemCounter[objname] = (int)itemCounter[objname] + 1;
        }
    }
     */

    void OnGUI()
    {
//        GUI.Label(new Rect(Screen.width - 100.0f, Screen.height-20.0f, 100.0f, 20.0f), "fps:" + frameRate.ToString());
        if (itemCounter.Count == 0) return;
        int count = 2;
        foreach( DictionaryEntry item in itemCounter ) {
            GUI.Label(new Rect(Screen.width - 100.0f, Screen.height - 20.0f * count, 200.0f, 20.0f), item.Key + ": " + item.Value);
            count++;
        }
    }
    
}
