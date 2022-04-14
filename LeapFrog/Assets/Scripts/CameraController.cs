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
    private int maxZoom = 16;
    private int minZoom = 5;

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
        currentPos.y = Math.Max(minY, target.position.y);
        mainTransform.position = currentPos;
        Vector3 stageDimensions = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        double zoomNum = mainCamera.orthographicSize;
        if (Input.mouseScrollDelta.y > 0 && zoomNum > minZoom)
        {
            minY -= 2f;
            mainCamera.orthographicSize -= 2;
            currentPos = new Vector3(mainTransform.position.x, Math.Max(minY, target.position.y - 2f), mainTransform.position.z);
        }
            

        else if (Input.mouseScrollDelta.y < 0 && zoomNum < maxZoom)
        {
            minY += 2f;
            mainCamera.orthographicSize += 2;
            currentPos = new Vector3(mainTransform.position.x, Math.Max(minY, target.position.y + 2f), mainTransform.position.z);
        }

       // mainTransform.position.SetY(Mathf.Clamp(stageDimensions.y, minY, 100)); 
    }
}
