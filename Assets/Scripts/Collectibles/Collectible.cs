using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Collectible : MonoBehaviour
{

    [SerializeField] private CollectibleType collectibleType;
    [SerializeField] private int amount;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    private TextMeshProUGUI amountText;
    public CollectibleType CollectibleType { get => collectibleType; }
    public int Amount { get => amount; }

    private void Awake()
    {
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Spawn(Vector2 position,int amount)
    {
        this.amount = amount;
        amountText.text = amount.ToString();
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds"))
        {
            gameObject.SetActive(false);
        }
    }
}
