using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Gameplay/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int waves;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int enemiesByWave;
    [SerializeField] private float timeBetweenEnemies;
    [SerializeField] private float enemySpeedMultiplier;
    [SerializeField] private AudioClip levelClip;

    public int Waves { get => waves; }
    public float TimeBetweenWaves { get => timeBetweenWaves; }
    public int EnemiesByWave { get => enemiesByWave; }
    public float TimeBetweenEnemies { get => timeBetweenEnemies; }
    public float EnemySpeedMultiplier { get => enemySpeedMultiplier; }

    public EnemyType EnemyType { get => enemyType; }
    public AudioClip LevelClip { get => levelClip; }


}
