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
    private ActiveSonar activeSonar = null;
    private GUITexture guiSonar = null;
    private SonarCamera sonarCamera = null;

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
        GameObject playerSonar = GameObject.Find("Player/ActiveSonar");
        if (playerSonar)
        {
            activeSonar = playerSonar.GetComponent<ActiveSonar>();
        }

        // �p�b�V�u�\�i�[���f�t�H���g
        SetMode(SonarMode.PassiveSonar);
    }

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

        // �X�N���[���T�C�Y�ɍ��킹�ăT�C�Y�E�ʒu����
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
