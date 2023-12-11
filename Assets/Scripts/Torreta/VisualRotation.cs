using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRotation : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        Vector3 targetOrientation = _target.position - transform.position;
        Debug.DrawRay(transform.position, targetOrientation, Color.red);

        Quaternion targetRotation = Quaternion.LookRotation(targetOrientation);

        // Limitar la rotación en el eje Y (inclinación)
        targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);

        transform.rotation = targetRotation;
    }
}
