using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

    [SerializeField]
    private GameObject activeObj;
    [SerializeField]
    private GameObject passiveObj;
    [SerializeField]
    private float aspect = 0.4f;

    private GameObject currentObj = null;
    private GUITexture guiSonar;
    private SonarCamera sonarCamera;

    public enum SonarMode {
        None,
        PassiveSonar,
        ActiveSonar
    }
    private SonarMode mode = SonarMode.None;

	void Start () 
    {
        guiSonar = GetComponent<GUITexture>();
        GameObject cameraObj = GameObject.Find("/Player/SonarCamera");
        if (cameraObj) {
            sonarCamera = cameraObj.AddComponent<SonarCamera>();
        }
        // パッシブソナーがデフォルト
        SetMode(SonarMode.PassiveSonar);
    }

//    void OnMouseDown()
//    {
//    }
//    void OnMouseUp()
//    {
//    }

    /*
    void OnMouseOver()
    {
    }
    void OnMouseExit()
    { 
    }
    void OnMouseEnter()
    { 
    }
     */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SetMode(SonarMode.ActiveSonar);
        if (Input.GetKeyUp(KeyCode.Space)) SetMode(SonarMode.PassiveSonar);
    }

    public Rect GetRect()
    {
        GUITexture guiSonar = GetComponent<GUITexture>();
        if (guiSonar) {
            return guiSonar.pixelInset;
        }
        return new Rect();
    }

    void SetMode( SonarMode mode_ )
    {
        if (mode == mode_) return;

        // スクリーンサイズに合わせてサイズ・位置調整
        if (currentObj != null)
        {
            Destroy(currentObj);
        }

        switch (mode_)
        {
            case SonarMode.ActiveSonar:
                CreateSonar(activeObj);
                break;

            case SonarMode.PassiveSonar:
                CreateSonar(passiveObj);
                break;

            default:
                guiSonar.enabled = false;
                break;
        }

        mode = mode_;
    }

    private void CreateSonar( GameObject obj ) 
    {
        float w = 0.0f;
        float h = 0.0f;
        float offset = 10.0f;

        guiSonar.enabled = true;
        w = Screen.height * aspect;
        h = w;
        guiSonar.pixelInset = new Rect(offset, Screen.height - offset - w, w, h);
        sonarCamera.SetRect(guiSonar.pixelInset);
        currentObj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        currentObj.transform.parent = transform;

        SonarEffect effecter = currentObj.GetComponent<SonarEffect>();
        effecter.Init(guiSonar.pixelInset);
   }
}
