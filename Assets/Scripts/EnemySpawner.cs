using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] EnemyManagerSO enemyManager;

    [Header("Movement")]
    [SerializeField] float minY;
    [SerializeField] float maxY;

        
    private int waves;
    private float timeBetweenWaves;
    private int enemiesByWave;
    private float timeBetweenEnemies;
    private float enemySpeedMultiplier;

    private void Awake()
    {
        waves = gameManager.CurrentLevel.Waves;
        timeBetweenWaves = gameManager.CurrentLevel.TimeBetweenWaves;
        enemiesByWave = gameManager.CurrentLevel.EnemiesByWave;
        timeBetweenEnemies = gameManager.CurrentLevel.TimeBetweenEnemies;
        enemySpeedMultiplier = gameManager.CurrentLevel.EnemySpeedMultiplier;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < waves; i++)
        {
            for(int j = 0; j < enemiesByWave; j++)
            {
                float yPosition = Random.Range(minY, maxY);
                transform.position = new Vector2(transform.position.x, yPosition);
                enemyManager.Spawn(transform.position);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }

            
        
    }
}
