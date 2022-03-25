using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private GameObject allyPrefab;
    private float squishLeeway = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Player")) 
        {
            if ((enemyTransform.position.y < collider.transform.position.y) && 
                Math.Abs(enemyTransform.position.x - collider.transform.position.x) < (enemyCollider.bounds.size.x * squishLeeway * 0.5))
            {
                PlayerController player = collider.GetComponentInParent<PlayerController>();
                player.addAlly(allyPrefab);
                Destroy(this.gameObject);
            }
         }
    }
}
