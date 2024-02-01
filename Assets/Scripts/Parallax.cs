using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float imageWidth;

    private Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = (speed * Time.time) % imageWidth;
        transform.position = initialPosition + delta * direction;
    }
}
