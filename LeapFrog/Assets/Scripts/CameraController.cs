using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = mainTransform.position;
        currentPos.x = target.position.x;
        mainTransform.position = currentPos;
    }
}
