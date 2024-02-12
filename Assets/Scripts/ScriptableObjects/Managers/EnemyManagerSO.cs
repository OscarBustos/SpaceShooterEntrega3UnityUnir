using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "Managers/Enemy Manager")]
public class EnemyManagerSO : ScriptableObject
{


    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int poolSize;
    private Dictionary<EnemyType, Enemy[]> pool;
    private Dictionary<EnemyType, int> enemyIndex;

    private void Init(EnemyType enemyType)
    {
        pool = new Dictionary<EnemyType, Enemy[]>();
        enemyIndex = new Dictionary<EnemyType, int>();
    
        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            Enemy[] enemies = new Enemy[poolSize];
            if (enemyPrefabs[i].GetComponent<Enemy>().EnemyType == enemyType)
            {
                for (int j = 0; j < poolSize; j++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[i], new Vector2(0, 0), Quaternion.identity);
                    enemies[j] = enemy.GetComponent<Enemy>();
                    enemy.SetActive(false);
                }
                pool.Add(enemyType, enemies);
                enemyIndex.Add(enemyType, 0);
            }
        }
    }

    public void Spawn(Vector2 position, EnemyType enemyType)
    {
        Enemy[] enemies = null;

        if (pool == null || !pool.TryGetValue(enemyType, out enemies) || pool[enemyType][0] == null)
        {
            Init(enemyType);
        }
        enemies = pool[enemyType];
        int currentIndex = enemyIndex[enemyType];
        Enemy enemy = enemies[currentIndex];
        Debug.Log("enemy on index "+ currentIndex +" null " + enemy == null);
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
