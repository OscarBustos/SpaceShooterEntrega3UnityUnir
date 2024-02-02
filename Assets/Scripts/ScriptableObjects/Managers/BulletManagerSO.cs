using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "BuletManager", menuName = "Managers/Bullet Manager")]
public class BulletManagerSO : ScriptableObject
{

    [Header("Pool")]

    [SerializeField] private int size;
    [SerializeField] private GameObject bulletPrefeb;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float xDirection;

    private Bullet[] bulletPool;
    private int currentIndex;

    private void Init()
    {
        bulletPool = new Bullet[size];
        for(int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefeb, Vector2.zero, Quaternion.identity);
            bulletPool[i] = bullet.GetComponent<Bullet>();
            bullet.SetActive(false);
        }
    }

    public void Shoot(Transform[] firePonintPositions)
    {
        if(bulletPool == null || bulletPool[0] == null)
        {
            Init();
        }
        foreach(Transform firePoint in firePonintPositions)
        {
            bulletPool[currentIndex].Move(speed, xDirection, firePoint.position);
            UpdateIndex();
        }
    }

    private void UpdateIndex()
    {
        currentIndex++;
        if (currentIndex > size - 1)
        {
            currentIndex = 0;
        }
    }
}
