using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private Transform target;
    [SerializeField] private float minY;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = mainTransform.position;
        currentPos.x = target.position.x;
        currentPos.y = Math.Max(minY, target.position.y);
        mainTransform.position = currentPos;
    }
}
