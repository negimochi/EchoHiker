using UnityEngine;
using System.Collections;

public class SonarSwitcher : MonoBehaviour
{

    [SerializeField]
    private GameObject activeObj;
    [SerializeField]
    private GameObject passiveObj;
    [SerializeField]
    private int offsetPixel = 10;   // ���[����̈ʒu�I�t�Z�b�g
    [SerializeField]
    private float aspect = 0.4f;    // ��ʂɑ΂���T�C�Y��
    [SerializeField]
    private int cameraRayoutPixel = 8;  // ���h����A�e�N�X�`���T�C�Y��菭�������̃T�C�Y�ŃJ�����ʒu�����߂�

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
//        GameObject playerSonar = GameObject.Find("Player/ActiveSonar");
//        if (playerSonar)
//        {
//            activeSonar = playerSonar.GetComponent<ActiveSonar>();
//        }

        SetPosition();
        // �p�b�V�u�\�i�[���f�t�H���g
        SetMode(SonarMode.PassiveSonar);
    }

    void Update()
    {
        // �����Ă�Ԃ����A�N�e�B�u�\�i�[
        if (Input.GetKeyDown(KeyCode.Space)) SetMode(SonarMode.ActiveSonar);
        if (Input.GetKeyUp(KeyCode.Space)) SetMode(SonarMode.PassiveSonar);
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

        // �J�����Ƀe�N�X�`���T�C�Y��`����
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
