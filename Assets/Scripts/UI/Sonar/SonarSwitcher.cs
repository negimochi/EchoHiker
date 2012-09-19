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
    [SerializeField]
    private string tagetCamera = "/Field/Player/SonarCamera";
    private GameObject currentObj = null;
//    private SonarCamera sonarCamera = null;

    public enum SonarMode {
        None,
        PassiveSonar,
        ActiveSonar
    }
    private SonarMode mode = SonarMode.None;

	void Start () 
    {
        OnStageReset();
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

    void OnGameStart()
    {
//        InitPosition();
    }

    void OnStageReset()
    {
        InitPosition();
        // パッシブソナーがデフォルト
        SetMode(SonarMode.PassiveSonar);
    }

    private void InitPosition()
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
        GameObject cameraObj = GameObject.Find(tagetCamera);
        if (cameraObj)
        {
            SonarCamera sonarCamera = cameraObj.AddComponent<SonarCamera>();
            sonarCamera.SetRect(cameraRect);
        }
    }

    private void CreateSonar( GameObject obj ) 
    {
        currentObj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        currentObj.transform.parent = transform;

        SonarEffect effecter = currentObj.GetComponent<SonarEffect>();
        effecter.Init(guiTexture.pixelInset);
   }
}
