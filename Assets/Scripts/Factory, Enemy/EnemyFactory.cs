using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract GameObject CreateEnemy(Vector3 position, int health, int damage);
}
