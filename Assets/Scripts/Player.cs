using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        bulletManager.Init();
        shootingTime = fireRatio;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameOver)
        {
            Move();
            Shoot();
        }
    }

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
}
