using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;

    public IEnemy CreateEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemy, position, Quaternion.identity);
        return enemyObj.GetComponent<Enemy>();
    }
    public IEnemy CreateEnemy2(Vector3 position)
    {
        GameObject enemyObj2 = Instantiate(enemy2, position, Quaternion.identity);
        return enemyObj2.GetComponent<IEnemy>();
    }
}
