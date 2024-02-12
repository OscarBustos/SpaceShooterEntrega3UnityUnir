using UnityEngine;

public class MovingEnemy : Enemy
{
    [Header("Vertical Movement")]
    [SerializeField] private float verticalSpeed = 1.0f;
    [SerializeField] private float verticalDistance = 1.0f;
    private int randomVerticalDirection;
    private int randomDistance;
    private bool applyVerticalMovement;
    private void Start()
    {
        randomVerticalDirection = Random.Range(-1, 1);
        if(randomVerticalDirection < 0)
        {
            randomVerticalDirection = -1;
        }else
        {
            randomVerticalDirection = 1;
        }
        randomDistance = Random.Range(1,(int) verticalDistance + 1);
        int randomInt = Random.Range(1, 100);
        applyVerticalMovement = ( randomInt > 0 && randomInt < 60);
    }

    public override void Move()
    {
        if (applyVerticalMovement)
        {
            float y = randomDistance * Mathf.Sin(Time.time * verticalSpeed);
            transform.position = new Vector3(transform.position.x, y * randomVerticalDirection, transform.position.z);
        }
        else
        {
            speed = 10;
            base.Move();
        }
        
        
    }
}
