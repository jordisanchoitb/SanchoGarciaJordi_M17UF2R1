using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Transform))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    private float smoothSpeed = float.MaxValue; 

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcular la posición deseada
            Vector3 desiredPosition = target.position + offset;

            // Suavizar el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualizar la posición de la cámara
            transform.position = smoothedPosition;
        }

    }
}
