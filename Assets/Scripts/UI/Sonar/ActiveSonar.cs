using UnityEngine;
using System.Collections;

public class ActiveSonar : MonoBehaviour {

//    [SerializeField]
    private float maxRadius = 300.0f;
    [SerializeField]
    private float delayTime = 0.2f;

//    private float currentTime = 0.0f;

    private GameObject player = null;
    private RandomGenerator enemy = null;
    private RandomGenerator item  = null;
    private TorpedoManager torpedo = null;
    private SonarEffect effect = null;

	void Start () 
    {
        effect = GetComponent<SonarEffect>();
        player = GameObject.Find("/Player");
        GameObject enemyObj = GameObject.Find("/Object/EnemyManager");
        if (enemyObj) enemy = enemyObj.GetComponent<RandomGenerator>();
        GameObject itemObj = GameObject.Find("/Object/ItemManager");
        if (itemObj) item = itemObj.GetComponent<RandomGenerator>();
        GameObject torpedoObj = GameObject.Find("/Object/TorpedoManager");
        if (torpedoObj) torpedo = torpedoObj.GetComponent<TorpedoManager>();
        GameObject sonarCameraObj = GameObject.Find("/Player/SonarCamera");
        if (sonarCameraObj) maxRadius = sonarCameraObj.GetComponent<SphereCollider>().radius;

        StartCoroutine("Delay");
	}

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);

        // 手抜き探索
        float effectDist = Mathf.Lerp(0.0f, maxRadius, effect.Value());
        //Debug.Log("ActiveSonar="+effectDist + ":" + Time.time);
        if (enemy)
        {
            //Debug.Log("Enemy Search :" + enemy.SonarChildren().Count);
            Search(enemy.SonarChildren(), effectDist);
        }
        if (item)
        {
            //Debug.Log("Item Search");
            Search(item.SonarChildren(), effectDist);
        }
        if (torpedo)
        {
            Debug.Log("SonarTorpedo:" + torpedo.SonarChildren().Count);
            Search(torpedo.SonarChildren(), effectDist);
        }

        StartCoroutine("Delay");
    }
	
    void Search(ArrayList array, float effectDist) 
    {
        //Debug.Log(array.Count);
        int i = 0;
        while (i < array.Count)
        {
            GameObject target = array[i] as GameObject;
            float dist = 0.0f;
            if ( target ) dist = Vector3.Distance(target.transform.position, player.transform.position);
            if (effectDist > dist)
            {
                // 指定距離以内だったらソナーがヒット
                if (target) target.BroadcastMessage("OnSonar", SendMessageOptions.DontRequireReceiver);
                array.RemoveAt(i);
            }
            else i++;
        }
    }
}
