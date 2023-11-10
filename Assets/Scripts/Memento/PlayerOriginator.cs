using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOriginator : MonoBehaviour
{
    public PlayerMemento Save()
    {
        return new PlayerMemento(gameObject.transform);
    }

    public void Restore(PlayerMemento memento)
    {
        gameObject.transform.position = new Vector3(memento.x, memento.y, memento.z);
    }
}
