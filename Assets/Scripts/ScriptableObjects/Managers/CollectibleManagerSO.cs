using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "CollectibleManager", menuName = "Managers/Collectible Manager")]
public class CollectibleManagerSO : ScriptableObject
{
    [SerializeField] GameObject[] collectiblePrefabs;
    [SerializeField] int poolSize;
    [Header("Probability of spawn for each preafab included")]
    [SerializeField] int[] prefabSpawnProbability;
    [SerializeField] int[] probabilityRange;

    private Dictionary<CollectibleType, Collectible[]> pool;
    private Dictionary<CollectibleType, int> collectibleIndex;
    public void Init()
    {
        pool = new Dictionary<CollectibleType, Collectible[]>();
        collectibleIndex = new Dictionary<CollectibleType, int>();

        for (int i = 0; i < collectiblePrefabs.Length; i++)
        {
            Collectible[] collecibles = new Collectible[poolSize];
            for (int j = 0; j < poolSize; j++)
            {
                GameObject collectible = Instantiate(collectiblePrefabs[i], new Vector2(0, 0), Quaternion.identity);
                collecibles[j] = collectible.GetComponent<Collectible>();
                collectible.SetActive(false);
            }
            CollectibleType collectibleType = collecibles[0].GetComponent<Collectible>().CollectibleType;
            pool.Add(collectibleType, collecibles);
            collectibleIndex.Add(collectibleType, 0);
        }
    }

    public void SpawnCollectible(Vector2 position, int coinsAmount,int livesAmount)
    {
        if(pool == null || pool[0][0] == null)
        {
            Init();
        }
        int randomValue = Random.Range(0, 100);
        
        for(int i = 0; i < prefabSpawnProbability.Length; i++)
        {
            int prefabProbability = prefabSpawnProbability[i];
            if ((randomValue >= (prefabProbability - probabilityRange[i])) && (randomValue <= (prefabProbability + probabilityRange[i])))
            {
                CollectibleType collectibleType = collectiblePrefabs[i].GetComponent<Collectible>().CollectibleType;
                int amount = SelectAmount(collectibleType, coinsAmount, livesAmount);
                Collectible[] collectibles = pool[collectibleType];
                collectibles[collectibleIndex[collectibleType]].Spawn(position, amount);
                collectibleIndex[collectibleType] = UpdateIndex(collectibleIndex[collectibleType], collectibles.Length);
            }
        }
    }

    private int SelectAmount(CollectibleType collectibleType, int coinsAmount, int livesAmount)
    {
        switch (collectibleType)
        {
            case CollectibleType.Coin: return coinsAmount;
            case CollectibleType.Live: return livesAmount;
            default: return 0;
        }
    }

    private int UpdateIndex(int currentIndex, int lenght)
    {
        currentIndex++;
        if (currentIndex > lenght - 1)
        {
            currentIndex = 0;
        }
        return currentIndex;
    }
}
