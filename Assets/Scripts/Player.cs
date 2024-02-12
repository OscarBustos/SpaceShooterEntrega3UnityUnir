using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] int lives = 3;
    
    [Header("Managers")]
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] BulletManagerSO bulletManager;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;

    [Header("Shooting")]
    [SerializeField] float fireRatio;
    [SerializeField] Transform[] firePointPositions;

    private Transform[] enabledFirePointsPositions;
    private float verticalDirection;
    private float horizontalDirection;
    private bool shoot;
    private float shootingTime;
    private Sprite skin;
    private int cannonNumber;

    private void Awake()
    {
        int savedLives = PlayerPrefs.GetInt("Lives");
        LoadPlayerSettings();
        if (savedLives == 0)
        {
            PlayerPrefs.SetInt("Lives", lives);
        }
        else
        {
            lives = savedLives;
        }
        gameManager.TotalLives = lives;
        
    }

    private void OnEnable()
    {
        gameManager.OnChangeLevel += OnChangeLevel;
        gameManager.OnReloadPlayerPrefs += LoadPlayerSettings;
    }

    private void OnDisable()
    {
        gameManager.OnChangeLevel -= OnChangeLevel;
        gameManager.OnReloadPlayerPrefs -= LoadPlayerSettings;
    }
    void Start()
    {
        shootingTime = fireRatio;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.PauseResumeGame();
            }
            Move();
            Shoot();
        }
    }

    #region Methods
    private void Move()
    {
        verticalDirection = Input.GetAxisRaw("Vertical");
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(horizontalDirection, verticalDirection).normalized * speed * Time.deltaTime);

        float xClamped = Mathf.Clamp(transform.position.x, minX, maxX);
        float yClamped = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector2(xClamped, yClamped);
    }

    private void Shoot()
    {
        CalculateShootingTime();
        if (Input.GetKey(KeyCode.Space) && shoot)
        {
            bulletManager.Shoot(enabledFirePointsPositions);
            shoot = false;
        }
    }

    private void CalculateShootingTime()
    {
        shootingTime += Time.deltaTime;
        if(shootingTime >= fireRatio)
        {
            shoot = true;
            shootingTime = 0;
        }
    }

    private void Collect(Collectible collectible)
    {
        switch (collectible.CollectibleType)
        {
            case CollectibleType.Coin: 
            {
                    gameManager.TotalCoins += collectible.Amount;
                    gameManager.LevelCoins += collectible.Amount;
                    break; 
            }
            case CollectibleType.Live:
            {
                    gameManager.TotalLives += collectible.Amount;                    
                    lives += collectible.Amount;
                    break;    
            }
        }
    }

    private void OnChangeLevel()
    {
        PlayerPrefs.SetInt("Lives", lives);
    }

    private void LoadPlayerSettings()
    {
        string skinName = PlayerPrefs.GetString("Skin");
        if(skinName != "")
        {
            skin = Resources.Load<Sprite>("Sprites/PlayerShips/" + skinName);
            GetComponentInChildren<SpriteRenderer>().sprite = skin;
        }
        cannonNumber = PlayerPrefs.GetInt("CannonNumber");
        

        switch (cannonNumber)
        {
            case 0:
            case 1:
                {
                    firePointPositions[0].gameObject.SetActive(true);
                    firePointPositions[1].gameObject.SetActive(false);
                    firePointPositions[2].gameObject.SetActive(false);
                    enabledFirePointsPositions = new Transform[] { firePointPositions[0] };
                    break;
                }
            case 2:
                {
                    firePointPositions[0].gameObject.SetActive(false);
                    firePointPositions[1].gameObject.SetActive(true);
                    firePointPositions[2].gameObject.SetActive(true);
                    enabledFirePointsPositions = new Transform[] { firePointPositions[1], firePointPositions[2] };
                    break;
                }
            case 3:
                {
                    firePointPositions[0].gameObject.SetActive(true);
                    firePointPositions[1].gameObject.SetActive(true);
                    firePointPositions[2].gameObject.SetActive(true);
                    enabledFirePointsPositions = firePointPositions;
                    break;
                }
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            lives--;
            gameManager.TotalLives = lives;
            collision.gameObject.SetActive(false);
            if(lives <= 0)
            {
                PlayerPrefs.SetInt("Lives", 0);
                gameManager.GameOver();
            }
        } 
        else if (collision.CompareTag("Collectible"))
        {
            Collect(collision.gameObject.GetComponent<Collectible>());
            collision.gameObject.SetActive(false);
        }
    }
    #endregion
}
