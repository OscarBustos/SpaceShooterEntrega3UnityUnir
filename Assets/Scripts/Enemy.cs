using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private Transform waypoints;

    [Header("Shooting")]
    [SerializeField] private bool shoot;
    [SerializeField] private float startShootingAfterSeconds;
    [SerializeField] private float fireRate;
    [SerializeField] private BulletManagerSO bulletManager;
    [SerializeField] private Transform[] firePoints;

    [Header("Collectibles")]
    [SerializeField] private int coinsAmount;
    [SerializeField] private CollectibleManagerSO collectibleManager;
    private int livesAmount = 1;
    public EnemyType EnemyType { get => enemyType; }


    #region Methods
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        bulletManager.Shoot(firePoints);
    }


    public void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        if (shoot)
        {
            InvokeRepeating("Shoot", startShootingAfterSeconds, fireRate);
        }
    }

    public void Die()
    {
        collectibleManager.SpawnCollectible(transform.position, coinsAmount, livesAmount);
    }
    #endregion

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool disable = false;
        if (collision.CompareTag("PlayerBullet"))
        {
            Die();
            disable = true;
        } else if (collision.CompareTag("Bounds"))
        {
            disable = true;
        }

        if (disable)
        {
            transform.position = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
