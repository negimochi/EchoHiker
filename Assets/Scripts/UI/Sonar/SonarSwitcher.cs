using UnityEngine;
using System.Collections;

public class SonarSwitcher : MonoBehaviour
{

    [SerializeField]
    private GameObject activeObj;
    [SerializeField]
    private GameObject passiveObj;
    [SerializeField]
    private int offsetPixel = 10;   // 左端からの位置オフセット
    [SerializeField]
    private float aspect = 0.4f;    // 画面に対するサイズ比
    [SerializeField]
    private int cameraRayoutPixel = 8;  // 見栄え上、テクスチャサイズより少し内側のサイズでカメラ位置を決める

    private GameObject currentObj = null;
//    private ActiveSonar activeSonar = null;
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
//        GameObject playerSonar = GameObject.Find("Player/ActiveSonar");
//        if (playerSonar)
//        {
//            activeSonar = playerSonar.GetComponent<ActiveSonar>();
//        }

        SetPosition();
        // パッシブソナーがデフォルト
        SetMode(SonarMode.PassiveSonar);
    }

    void Update()
    {
        // 押してる間だけアクティブソナー
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
                break;

            case SonarMode.PassiveSonar:
                CreateSonar(passiveObj);
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

        // カメラにテクスチャサイズを伝える
        Rect cameraRect = new Rect(guiTexture.pixelInset);
        cameraRect.x += cameraRayoutPixel;
        cameraRect.y += cameraRayoutPixel;
        cameraRect.width -= cameraRayoutPixel * 2;
        cameraRect.height -= cameraRayoutPixel * 2;
        sonarCamera.SetRect(cameraRect);
    }

    private void CreateSonar( GameObject obj ) 
    {
        currentObj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        currentObj.transform.parent = transform;

        SonarEffect effecter = currentObj.GetComponent<SonarEffect>();
        effecter.Init(guiTexture.pixelInset);
   }
}
