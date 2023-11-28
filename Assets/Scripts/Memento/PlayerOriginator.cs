using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOriginator : MonoBehaviour
{
    /*public PlayerMemento Save()
    {
        return new PlayerMemento(gameObject.transform);
    }

    public void Restore(PlayerMemento memento)
    {
        gameObject.transform.position = new Vector3(memento.x, memento.y, memento.z);
    }*/

    public PlayerMemento Save()
    {
        return new PlayerMemento(gameObject.transform);
    }

    public void Restore(PlayerMemento memento)
    {
        memento.Restore(this);
    }

    public class PlayerMemento
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

        public void Restore(PlayerOriginator originator)
        {
            originator.transform.position = new Vector3(x, y, z);
        }
    }

}
