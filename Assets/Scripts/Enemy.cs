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

    public EnemyType EnemyType { get => enemyType; }

    void Start()
    {
        if (shoot)
        {
            if (!bulletManager.IsInitialized)
            {
                bulletManager.Init();
            }
            InvokeRepeating("Shoot", startShootingAfterSeconds, fireRate);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        bulletManager.Shoot(firePoints);
    }


}
