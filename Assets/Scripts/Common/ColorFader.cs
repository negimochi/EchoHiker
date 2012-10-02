using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

    [SerializeField]
    private float duration = 2.0f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float minAlpha = 0.1f;

    [SerializeField]
    private bool sonarHit = false;
    [SerializeField]
    private bool sonarInside = false;

    private bool wait = false;
    private float max = 0.0f;
    private float currentTime = 0.0f;
    private Color startColor;

	void Start () 
    {
        max = 1.0f - minAlpha;
        startColor = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a);

        // 生成された段階で自分がソナー内にいるかチェック
        GameObject player = GameObject.Find("/Field/Player");
        if (player)
        {
            Debug.Log("ColorFader.OnInstantiatedSonarPoint");
            player.BroadcastMessage("OnInstantiatedSonarPoint", gameObject);
        }
	}

	void Update () 
    {
        if (!renderer.enabled) return;

        if (!wait)
        {
            float time = currentTime / duration;
            if (time <= (2.0f*max))
            {
                float alpha = 1.0f - Mathf.PingPong(time, max);
                renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                // 時間更新
                currentTime += Time.deltaTime;
            }
            else
            {
                wait = true;
                StartCoroutine("Delay", delay);
            }
        }
	}

    void OnHit()
    {
        // ヒットした瞬間でソナーから見えなくする
        Debug.Log(gameObject.transform.parent.gameObject.name + " -> OnHit");
        sonarHit = false;
        Enable();
    }

    void OnActiveSonar()
    {
        // ソナーから見えることを許可する
        Debug.Log(gameObject.transform.parent.gameObject.name + " -> ActiveSonar");
        sonarHit = true;
        Enable();
    }

    void OnSonarInside()
    {
        // ソナー表示領域の内側
        Debug.Log(gameObject.transform.parent.gameObject.name + " -> SonarInside");
        sonarInside = true;
        Enable();
    }

    void OnSonarOutside()
    {
        // ソナー表示領域の外側
        Debug.Log(gameObject.transform.parent.gameObject.name + " -> SonarOutside");
        sonarInside = false;
        Enable();
    }

    private void Enable()
    {
        Debug.Log(gameObject.transform.parent.gameObject.name + ": sonarInside=" + sonarInside + ", sonarHit=" + sonarHit);
        bool result = (sonarInside && sonarHit)?true:false;
        renderer.enabled = result;
        if (result)
        {
            wait = false;
            currentTime = 0.0f;
        }
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
        currentTime = 0.0f;
    }

    public bool SonarInside() {
        return sonarInside;
    }
}
