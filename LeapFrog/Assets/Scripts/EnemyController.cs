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

    private float lastHitTime;
    //Returns true if the enemy was jumped on and killed by the player, false otherwise.
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
            else if (collision.gameObject.CompareTag("Player") && (Time.time - lastHitTime > 1f))
            {
                lastHitTime = Time.time;
                Destroy(this.gameObject);
                if (PlayerController.instance.Health > 0)
                {
                    List<GameObject> stack = PlayerController.instance.stack;
                    if(stack.Count == 1)
                    {
                        PlayerController.instance.Health--;
                        Debug.Log(PlayerController.instance.Health);
                    }
                    else if(stack.Count == 2)
                    {

                        removeAlly(stack, stack.Count-1);
                    }
                    else if(stack.Count > 2 && stack.Count <= 14)
                    {
                        int numToRemove = (int)Mathf.Floor(Mathf.Sqrt(stack.Count - 2));
                        int stackCount = stack.Count;
                        for(int i = stackCount - 1; i > stackCount - 1 - numToRemove; i--)
                        {
                            removeAlly(stack, i);
                        }
                    }
                    else if(stack.Count > 14)
                    {
                        int removeMax = 5;
                        for (int i = stack.Count - 1; i > stack.Count - 1 - removeMax; i--)
                        {
                            removeAlly(stack, i);
                        }
                    }
                    
                }
            }
        }
    }

    private static void removeAlly(List<GameObject> stack, int i)
    {
        GameObject removedAlly = stack[i];
        Vector3 removeHeight = new Vector3(0, removedAlly.GetComponent<BoxCollider2D>().bounds.size.y * 1.5f, 0);
        Destroy(stack[i]);
        stack.RemoveAt(i);
        PlayerController.instance.topOfStack -= removeHeight;
        PlayerController.instance.whatAlly(removedAlly, false);
    }
}
