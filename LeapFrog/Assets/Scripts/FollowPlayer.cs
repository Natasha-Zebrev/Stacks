using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform mainTransform;
    [SerializeField] private int moveSpeed;
    private bool playerToLeft;
    void Update()
    {
        playerToLeft = PlayerController.instance.transform.position.x < mainTransform.position.x;
        moveToPlayer();
    }

    private void moveToPlayer()
    {

        faceCorrectDirection(playerToLeft);

        Vector3 playerPos = PlayerController.instance.transform.position;

        Vector3 directionToPlayer = (playerPos - mainTransform.position).normalized;
        mainTransform.position += (directionToPlayer * moveSpeed * Time.deltaTime).SetY(0);
    }

    private void faceCorrectDirection(bool playerLeft)
    {
        if(playerLeft)
        {
            correctDirection(0);
        }
        else if(!playerLeft)
        {
            correctDirection(180);
        }
    }

    private void correctDirection(int rotateX)
    {
        Vector3 currentRotation = mainTransform.rotation.eulerAngles;
        currentRotation.y = rotateX;
        mainTransform.eulerAngles = currentRotation;
    }
}
