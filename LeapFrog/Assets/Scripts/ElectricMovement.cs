using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMovement : MonoBehaviour
{
    [SerializeField] private Transform electricTransform;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private int sideToSideDistance;
    [SerializeField] private bool flippedSprite;
    [SerializeField] private float lungeDistance;
    private float startingX;
    private bool movingLeft = true;
    private float minX;
    private float maxX;
    private bool waitTrigger = false;

    void Start()
    {
        startingX = electricTransform.position.x;
        minX = startingX - sideToSideDistance;
        maxX = startingX + sideToSideDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTrigger == false)
        {
            moveElectricEnemy();
            StartCoroutine(Wait());
        }
    }

    private void moveElectricEnemy()
    {
        Vector3 enemyPos = electricTransform.position;
            if (enemyPos.x - minX <= 0.05)
            {
                movingLeft = false;
            }
            else if (enemyPos.x - maxX >= 0.05)
            {
                movingLeft = true;
            }
            //Electric enemy movement is covered here.
            if(movingLeft && enemyPos.x >= minX && waitTrigger == false)
            {
                faceCorrectDirection(movingLeft);
                Vector3 lunge = new Vector3(-lungeDistance,0,0);
                electricTransform.position += lunge;
                waitTrigger = true;
            }
            else if(!movingLeft && enemyPos.x <= maxX && waitTrigger == false)
            {
                faceCorrectDirection(movingLeft);
                Vector3 lunge = new Vector3(lungeDistance,0,0);
                electricTransform.position += lunge;
                waitTrigger = true;
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
        {
            movingLeft = !movingLeft;
        }
    }

    private void faceCorrectDirection(bool directionMovingLeft)
    {
        if (!flippedSprite)
            mainRenderer.flipX = !directionMovingLeft;
        else
            mainRenderer.flipX = directionMovingLeft;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        waitTrigger = false;
    }

}
