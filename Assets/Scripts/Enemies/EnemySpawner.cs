using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] EnemyManagerSO enemyManager;
    [SerializeField] LevelManager levelManager;
    [SerializeField] float startSpawningAfter;

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
        levelManager.CurrentWave = 1;
        gameManager.ChangeWave();
        yield return new WaitForSeconds(startSpawningAfter);
        for(int i = 0; i < waves; i++)
        {
            
            for (int j = 0; j < enemiesByWave; j++)
            {
                float yPosition = Random.Range(minY, maxY);
                transform.position = new Vector2(transform.position.x, yPosition);
                enemyManager.Spawn(transform.position, gameManager.CurrentLevel.EnemyType);
                yield return new WaitForSeconds(timeBetweenEnemies);
                
            }
            
            if(levelManager.CurrentWave < waves)
            {
                levelManager.CurrentWave++;
                gameManager.ChangeWave();
            }
            if(i < waves - 1)
                yield return new WaitForSeconds(timeBetweenWaves);   
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(ChangeLevelWhenEndOfLevel());
    }

    public IEnumerator ChangeLevelWhenEndOfLevel()
    {
        bool changeLevel = true;
        while (changeLevel)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Collectible[] collectibles = FindObjectsOfType<Collectible>();
            if (enemies.Length == 0 && collectibles.Length == 0)
            {
                changeLevel = false;
                gameManager.ChangeLevel();
            }
            yield return new WaitForSeconds(0.5f);
        }
         
    }
    
}
