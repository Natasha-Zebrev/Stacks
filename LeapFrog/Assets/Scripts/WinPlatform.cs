using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinPlatform : MonoBehaviour
{
    [SerializeField] private GameObject winPlat;
    [SerializeField] private BoxCollider2D platCollider;
    [SerializeField] private Transform platTransform;

    private float squishLeeway = 1.4f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float playerPos = PlayerController.instance.playerTransform.position.y;
        int playerStackCount = PlayerController.instance.stack.Count - 1;
        GameObject collider = collision.gameObject;
        if(collider.CompareTag("Player"))
        {
            if((platTransform.position.y < collider.transform.position.y)
            && Math.Abs(platTransform.position.x - collider.transform.position.x) <
            (platCollider.bounds.size.x * squishLeeway * 0.5) && playerPos > platTransform.position.y + 1)
            {
                GameUI.instance.CheckWin(playerStackCount);
            }
        }
    }
}
