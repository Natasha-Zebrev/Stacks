using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private GameObject allyPrefab;
    private float squishLeeway = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float lastHitTime;
    //Returns true if the enemy was jumped on and killed by the player, false otherwise.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Player")) 
        {
            if ((enemyTransform.position.y < collider.transform.position.y) && 
                Math.Abs(enemyTransform.position.x - collider.transform.position.x) < (enemyCollider.bounds.size.x * squishLeeway))
            {
                PlayerController player = collider.GetComponentInParent<PlayerController>();
                player.addAlly(allyPrefab);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.CompareTag("Player") && (Time.time - lastHitTime > 1f))
            {
                lastHitTime = Time.time;
                Destroy(this.gameObject);
                if (PlayerController.instance.Health > 0)
                {
                    List<GameObject> stack = PlayerController.instance.stack;
                    int stackCount = stack.Count;
                    if(stackCount == 1)
                    {
                        PlayerController.instance.Health--;
                        Debug.Log(PlayerController.instance.Health);
                    }
                    else if(stackCount == 2)
                    {

                        PlayerController.instance.removeAlly(stackCount - 1);
                    }
                    else if(stackCount > 2 && stackCount <= 14)
                    {
                        int numToRemove = (int)Mathf.Floor(Mathf.Sqrt(stackCount - 2));
                        for(int i = stackCount - 1; i > stackCount - 1 - numToRemove; i--)
                        {
                            PlayerController.instance.removeAlly(i);
                        }
                    }
                    else if(stackCount > 14)
                    {
                        int removeMax = 5;
                        for (int i = stackCount - 1; i > stackCount - 1 - removeMax; i--)
                        {
                            PlayerController.instance.removeAlly(i);
                        }
                    }
                    
                }
            }
        }
    }

   
}
