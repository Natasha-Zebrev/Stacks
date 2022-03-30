using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform mainTransform;
    [SerializeField] private int moveSpeed;
    void Update()
    {
        moveToPlayer();
    }

    private void moveToPlayer()
    {
        Vector3 playerPos = PlayerController.instance.transform.position;
        //mainTransform.LookAt(playerPos);

        Vector3 currentRotation = mainTransform.rotation.eulerAngles;
        currentRotation.x = 0;
        mainTransform.eulerAngles = currentRotation;

        Vector3 directionToPlayer = (playerPos - mainTransform.position).normalized;
        mainTransform.position += (directionToPlayer * moveSpeed * Time.deltaTime);
    }
}
