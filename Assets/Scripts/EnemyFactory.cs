using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public Enemy CreateBasicEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemy, position, Quaternion.identity);
        return enemyObj.GetComponent<Enemy>();
    }
    public IEnemy CreateEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemy, position, Quaternion.identity);
        return enemyObj.GetComponent<IEnemy>();
    }
}
