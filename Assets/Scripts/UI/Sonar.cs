using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

    [SerializeField]
    private float modeEffectTime = 60.0f;
    [SerializeField]
    private float aspect = 0.4f;
   
    private GUITexture effectTexture;
    private GameObject effecter;

    public enum SonarMode {
        None,
        PassiveSonar,
        ActiveSonar
    }
    private SonarMode mode = SonarMode.None;

	void Start () {
        //effectTexture = gameObject.GetComponentInChildren<GUITexture>();
        // パッシブソナーがデフォルト
        SetMode(SonarMode.PassiveSonar);
    }

    void SetMode( SonarMode mode_ )
    {
        if (mode == mode_) return;

        // スクリーンサイズに合わせてサイズ・位置調整
        GUITexture guiSonar = GetComponent<GUITexture>();
        float w = 0.0f;
        float h = 0.0f;
        float offset = 10.0f;
        if (effecter != null) {
            GameObject.Destroy(effecter);
        }
        switch (mode_)
        {
            case SonarMode.ActiveSonar:
                guiSonar.enabled = true;
                /*
            GameObject newItem = Object.Instantiate(itemObject[index], pos, Quaternion.identity) as GameObject;

                ActiveSonarEffect activeSE = gameObject.AddComponent<ActiveSonarEffect>();
                activeSE.Init(effectTexture, guiSonar.pixelInset, modeEffectTime);
                effecter = activeSE;
                 */
                break;

            case SonarMode.PassiveSonar:
                /*
                guiSonar.enabled = true;
                w = Screen.height * aspect;
                h = w;
                guiSonar.pixelInset = new Rect(offset, Screen.height - offset - w, w, h);
                PassiveSonarEffect passiveSE = gameObject.AddComponent<PassiveSonarEffect>();
                passiveSE.Init(effectTexture, guiSonar.pixelInset, modeEffectTime);
                effecter = passiveSE;
                 */
                break;

            default:
                guiSonar.enabled = false;
                break;
        }

        mode = mode_;
    }

        /*
    void FixedUpdate()
    {
        switch (mode)
        {
            case SonarMode.ActiveSonar:
                counter += Time.deltaTime;
                break;
            case SonarMode.PassiveSonar:
                counter -= Time.deltaTime;
                break;
        }

        if (counter > updateTime)
        {
            counter = 0;
            effect.pixelInset = new Rect(sonar.center.x, sonar.center.y, 0, 0);
        }
        else
        {
            float ratio = counter / updateTime;
            float newWidth = sonar.width * ratio;
            float newHeight = sonar.height * ratio;
            effect.pixelInset = new Rect(sonar.center.x - newWidth / 2.0f, sonar.center.y - newHeight / 2.0f, newWidth, newHeight);
            effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, 1.0f - ratio);
        }
    }
         */

}
