using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int sideToSideDistance;
    [SerializeField] private bool flippedSprite;
    private float startingX;
    private bool movingLeft = true;
    private float minX;
    private float maxX;

    void Start()
    {
        startingX = mainRigidbody.position.x;
        minX = startingX - sideToSideDistance;
        maxX = startingX + sideToSideDistance;
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    private void moveEnemy()
    {
        Vector3 enemyPos = mainRigidbody.position;
        
        if (enemyPos.x - minX <= 0.05)
        {
            movingLeft = false;
        }
        else if (enemyPos.x - maxX >= 0.05)
        {
            movingLeft = true;
        }

        if(movingLeft && enemyPos.x >= minX)
        {
            faceCorrectDirection(movingLeft);
            mainRigidbody.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0));
        }
        else if(!movingLeft && enemyPos.x <= maxX)
        {
            faceCorrectDirection(movingLeft);
            mainRigidbody.AddForce(new Vector2(+moveSpeed * Time.deltaTime, 0));
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

}
