using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

    [SerializeField]
    private GameObject activeObj;
    [SerializeField]
    private GameObject passiveObj;
    [SerializeField]
    private int offsetPixel = 10;
    [SerializeField]
    private float aspect = 0.4f;

    private GameObject currentObj = null;
    private ActiveSonar activeSonar = null;
    private SonarCamera sonarCamera = null;

    public enum SonarMode {
        None,
        PassiveSonar,
        ActiveSonar
    }
    private SonarMode mode = SonarMode.None;

	void Start () 
    {
        GameObject cameraObj = GameObject.Find("/Player/SonarCamera");
        if (cameraObj) {
            sonarCamera = cameraObj.AddComponent<SonarCamera>();
        }
        GameObject playerSonar = GameObject.Find("Player/ActiveSonar");
        if (playerSonar)
        {
            activeSonar = playerSonar.GetComponent<ActiveSonar>();
        }

        SetPosition();
        // パッシブソナーがデフォルト
        SetMode(SonarMode.PassiveSonar);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SetMode(SonarMode.ActiveSonar);
        if (Input.GetKeyUp(KeyCode.Space)) SetMode(SonarMode.PassiveSonar);
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
                activeSonar.Search();
                break;

            case SonarMode.PassiveSonar:
                CreateSonar(passiveObj);
                activeSonar.Reset();
                break;

            default:
                guiTexture.enabled = false;
                break;
        }

        mode = mode_;
    }

    private void SetPosition()
    {
        float size = Screen.height * aspect;

        guiTexture.enabled = true;
        guiTexture.pixelInset = new Rect(offsetPixel, Screen.height - offsetPixel - size, size, size);

        // カメラにサイズを伝える
        sonarCamera.SetRect(guiTexture.pixelInset);
    }

    private void CreateSonar( GameObject obj ) 
    {
        currentObj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        currentObj.transform.parent = transform;

        SonarEffect effecter = currentObj.GetComponent<SonarEffect>();
        effecter.Init(guiTexture.pixelInset);
   }
}
