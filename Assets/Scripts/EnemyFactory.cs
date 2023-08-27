using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemy;

    public Enemy CreateBasicEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemy, position, Quaternion.identity);
        return enemyObj.GetComponent<Enemy>();
    }

}
