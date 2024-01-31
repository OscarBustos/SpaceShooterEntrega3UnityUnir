using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool move;
    private float speed;
    private float xDirection;

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(new Vector2(xDirection, 0).normalized * speed * Time.deltaTime);
        }
    }

    public void Move(float speed, float xDirection, Vector2 position)
    {
        transform.position = position;
        this.speed = speed;
        this.xDirection = xDirection;
        move = true;
        gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        move = false;
        transform.position = Vector2.zero;
        gameObject.SetActive(false);
    }
}
