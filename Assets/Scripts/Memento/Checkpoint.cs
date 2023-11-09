using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public bool checkpointActivated = false;
    private Vector3 checkpointPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        checkpointActivated = true;
        checkpointPosition = transform.position;
        Debug.Log("Checkpoint Activated at: " + checkpointPosition);
    }

    public bool IsCheckpointActivated()
    {
        return checkpointActivated;
    }

    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }
}