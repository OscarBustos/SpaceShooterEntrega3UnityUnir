using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "Managers/Enemy Manager")]
public class EnemyManagerSO : ScriptableObject
{


    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int poolSize;
    private Dictionary<EnemyType, Enemy[]> pool;
    private Dictionary<EnemyType, int> enemyIndex;

    public void Init()
    {
        if(pool != null)
        {
            pool.Clear();
        }
        else
        {
            pool = new Dictionary<EnemyType, Enemy[]>();
        }

        Enemy[] enemies = new Enemy[poolSize];

        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            for(int j = 0; j < poolSize; j++)
            {

                GameObject enemy = Instantiate(enemyPrefabs[i], new Vector2(0, 0), Quaternion.identity);
                enemies[i] = enemy.GetComponent<Enemy>();
                enemy.SetActive(false);
            }
            EnemyType enemyType = enemies[0].GetComponent<Enemy>().EnemyType;
            pool.Add(enemyType, enemies);
            enemyIndex.Add(enemyType, 0);
        }
    }

    public void Spawn(Vector2 position)
    {

    }
}
