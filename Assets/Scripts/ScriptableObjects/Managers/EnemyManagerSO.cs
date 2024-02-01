using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            enemyIndex.Clear();
        }
        else
        {
            pool = new Dictionary<EnemyType, Enemy[]>();
            enemyIndex = new Dictionary<EnemyType, int>();
        }

        

        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            Enemy[] enemies = new Enemy[poolSize];
            for (int j = 0; j < poolSize; j++)
            {

                GameObject enemy = Instantiate(enemyPrefabs[i], new Vector2(0, 0), Quaternion.identity);
                enemies[j] = enemy.GetComponent<Enemy>();
                enemy.SetActive(false);
            }
            EnemyType enemyType = enemies[0].GetComponent<Enemy>().EnemyType;
            pool.Add(enemyType, enemies);
            enemyIndex.Add(enemyType, 0);
        }
    }

    public void Spawn(Vector2 position, EnemyType enemyType)
    {
        Enemy[] enemies = pool[enemyType];
        int currentIndex = enemyIndex[enemyType];
        Enemy enemy = enemies[currentIndex];
        enemy.Spawn(position);
        enemyIndex[enemyType] = UpdateIndex(currentIndex, enemies.Length);
            
    }

    private int UpdateIndex(int currentIndex, int lenght)
    {
        currentIndex++;
        if (currentIndex > lenght - 1)
        {
            currentIndex = 0;
        }
        return currentIndex;
    }
}
