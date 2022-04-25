using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSideToSide : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int sideToSideDistance;
    private float startingX;
    private bool movingLeft = true;
    private float minX;
    private float maxX;

    void Start()
    {
        startingX = mainTransform.position.x;
        minX = startingX - sideToSideDistance;
        maxX = startingX + sideToSideDistance;
        //Making it so the obstacle moves that distance every 60 frames (~1 second)
        moveSpeed /= 60;
    }

    // Update is called once per frame
    void Update()
    {
        moveObstacle();
    }

    private void moveObstacle()
    {
        Vector3 obsPos = mainTransform.position;

        if (obsPos.x - minX <= 0.05)
        {
            movingLeft = false;
        }
        else if (obsPos.x - maxX >= 0.05)
        {
            movingLeft = true;
        }

        if (movingLeft)
        {
            mainTransform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (!movingLeft)
        {
            mainTransform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}