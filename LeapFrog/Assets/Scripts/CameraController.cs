using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Transform mainTransform;
    [SerializeField] private Transform target;
    [SerializeField] private float minY;
    private Vector3 currentPos;

    void Start()
    {
        mainTransform = mainCamera.transform;
        currentPos = mainTransform.position;
        currentPos.y = Math.Max(minY, target.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPos.x = target.position.x;
        mainTransform.position = currentPos;
        Vector3 stageDimensions = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Debug.Log(stageDimensions.y);
        if (Input.mouseScrollDelta.y > 0)
            mainCamera.orthographicSize -= 2;

        else if (Input.mouseScrollDelta.y < 0)
        {
            mainCamera.orthographicSize += 2;
            mainCamera.transform.position += mainTransform.position.SetY(currentPos.y * Mathf.Abs(stageDimensions.y));
        }

       // mainTransform.position.SetY(Mathf.Clamp(stageDimensions.y, minY, 100)); 
    }
}
