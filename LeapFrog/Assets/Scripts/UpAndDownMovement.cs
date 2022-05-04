using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{

    [SerializeField] private Transform mainTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float upAndDownDistance;


    private float startingY;
    private float minY;
    private float maxY;
    private bool movingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        startingY = mainTransform.position.y;
        minY = startingY - upAndDownDistance;
        maxY = startingY + upAndDownDistance;

    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    private void moveEnemy()
    {
        Vector3 enemyPos = mainTransform.position;

        if (enemyPos.y - minY <= 0.05)
        {
            movingUp = true;
        }
        else if (enemyPos.y - maxY >= 0.05)
        {
            movingUp = false;
        }

        if (!movingUp)
        {
            mainTransform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        else if (movingUp)
        {
            mainTransform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
}
