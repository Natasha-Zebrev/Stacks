using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int sideToSideDistance;
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
        if (Mathf.Abs(enemyPos.x - minX) <= 0.05)
            movingLeft = false;
        else if (Mathf.Abs(enemyPos.x - maxX) <= 0.05)
            movingLeft = true;

        Debug.Log(movingLeft && enemyPos.x > minX);
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

    private void faceCorrectDirection(bool directionMovingLeft)
    {
        if (directionMovingLeft)
        {
            correctDirection(0);
        }
        else if (!directionMovingLeft)
        {
            correctDirection(180);
        }
    }

    private void correctDirection(int rotateX)
    {
       // Vector3 currentRotation = mainRigidbody.rotation.eulerAngles;
       // currentRotation.y = rotateX;
        //mainRigidbody.eulerAngles = currentRotation;
    }
}
