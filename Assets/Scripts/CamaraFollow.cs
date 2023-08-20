using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float angle = 30.0f;

    private void Update()
    {
        if(playerTransform == null)
        {
            Debug.Log("Player reference miss");
            return;
        }

        Vector3 offset = Quaternion.Euler(angle, 0, 0) * Vector3.back * distance;
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform.position);
    }

}
