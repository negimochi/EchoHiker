﻿using UnityEngine;
using System.Collections;

public class ItemCollider : MonoBehaviour
{
    [SerializeField]
    private bool valid = true;  // 念のためフラグ管理しておく

    void Start()
    {
    }

    void OnDestroyObject()
    {
        // 親から消してもらう
        transform.parent.gameObject.SendMessage("OnDestroyObject", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckPlayer(collision.gameObject);
    }
    void OnCollisionStay(Collision collider)
    {
        CheckPlayer(collider.gameObject);
    }

    void CheckPlayer(GameObject target)
    {
        if (! valid) return;
        if (!target.CompareTag("Player")) return;
        // Colliderを切る
        collider.enabled = false;
        valid = false;
        // プレイヤーと接触した際の効果
        SendMessage("OnRecovery");
    }

}
