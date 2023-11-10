using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    public PlayerMemento(Transform playerTransform)
    {
        this.x = playerTransform.position.x;
        this.y = playerTransform.position.y;
        this.z = playerTransform.position.z;
    }
}
