using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

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


    private float verticalDirection;
    private float horizontalDirection;
    private bool shoot;
    private float shootingTime;

    private void Awake()
    {
        int savedLives = PlayerPrefs.GetInt("Lives");
        if (savedLives == 0)
        {
            PlayerPrefs.SetInt("Lives", lives);
        }
        else
        {
            lives = savedLives;
        }
        
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
            bulletManager.Shoot(firePointPositions);
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
            case CollectibleType.Coin: {
                    gameManager.TotalCoins += collectible.Amount;
                    Debug.Log("coins " + gameManager.TotalCoins);
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
