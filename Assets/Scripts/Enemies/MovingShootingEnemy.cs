using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShootingEnemy : MovingEnemy
{
    [Header("Shooting")]
    [SerializeField] private bool shoot;
    [SerializeField] private float startShootingAfterSeconds;
    [SerializeField] private float fireRate;
    [SerializeField] private BulletManagerSO bulletManager;
    [SerializeField] private Transform[] firePoints;

    private void Shoot()
    {
        bulletManager.Shoot(firePoints);
    }


    public override void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        if (shoot)
        {
            InvokeRepeating("Shoot", startShootingAfterSeconds, fireRate);
        }
    }
}
