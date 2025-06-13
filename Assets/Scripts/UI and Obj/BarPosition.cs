using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera = 2f;

    void Update()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        transform.position = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0);
    }
}


