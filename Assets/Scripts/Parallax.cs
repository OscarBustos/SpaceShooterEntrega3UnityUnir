using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float imageWidth;
    [SerializeField] private GameManagerSO gameManager;

    private Vector2 initialPosition;
    private Animator animator;

    private static bool speedUpActivated;

    private void OnEnable()
    {
        gameManager.OnChangeLevel += OnChangeLevel;
    }

    private void OnDisable()
    {
        gameManager.OnChangeLevel -= OnChangeLevel;
    }

    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponentInParent<Animator>();
        speedUpActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = (speed * Time.time) % imageWidth;
        transform.position = initialPosition + delta * direction;
    }

    private void OnChangeLevel()
    {
        if (!speedUpActivated)
        {
            speedUpActivated = true;
            animator.SetTrigger("SpeedUp");
        }
    }
}
