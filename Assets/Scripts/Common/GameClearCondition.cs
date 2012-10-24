using UnityEngine;
using System.Collections;

public class GameClearCondition : MonoBehaviour
{
    [SerializeField]
    private bool valid = false;
    [SerializeField]
    private int destoryNorma = 0;
    [SerializeField]
    private int hitNorma = 0;

    private GameObject field = null;


    void Start()
    {
        field = GameObject.Find("/Field");
    }

    void OnInstantiatedChild(GameObject target)
    {
        // 生成したタイミング
        // 1つでも生成されば許可
    }

    void OnDestroyObject(GameObject target)
    {
        if (!valid) return;
        if (destoryNorma == 0) return;

        // 破棄されたタイミング
        destoryNorma--;
        if (destoryNorma <= 0) Clear(target.tag);
    }

    void OnHitObject(string tag)
    {
        if (!valid) return;
        if (hitNorma == 0) return;

        // ヒットしたタイミング
        hitNorma--;
        if (hitNorma <= 0) Clear(tag);
    }

    void OnLostObject(string tag)
    {
        // ロストしたタイミング
    }

    private void Clear( string tag )
    {
        if (field) field.SendMessage("OnClearCondition", tag);
        valid = false;
    }
}
