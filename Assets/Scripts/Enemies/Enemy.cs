using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    [Header("Movement")]
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected float speed;


    [Header("Collectibles")]
    [SerializeField] private int coinsAmount;
    [SerializeField] private CollectibleManagerSO collectibleManager;
    private int livesAmount = 1;
    public EnemyType EnemyType { get => enemyType; }


    #region Methods
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
    public virtual void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
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
            CancelInvoke("Shoot");
            transform.position = Vector2.zero;

            gameObject.SetActive(false);
        }
    }
    #endregion
}
