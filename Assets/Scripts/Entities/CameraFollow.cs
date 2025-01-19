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
            // Calcular la posici�n deseada
            Vector3 desiredPosition = target.position + offset;

            // Suavizar el movimiento de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualizar la posici�n de la c�mara
            transform.position = smoothedPosition;
        }

    }
}
