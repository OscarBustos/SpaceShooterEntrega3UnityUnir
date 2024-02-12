using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Store/Item List")]
public class StoreItemsListSO : ScriptableObject
{
    [SerializeField] private StoreItemSO[] storeList;
    [SerializeField] private StoreItemSO[] mutableStoreList;

    public StoreItemSO[] MutableList { get => mutableStoreList; set => mutableStoreList = value; }

    public void SetList()
    {
        if (mutableStoreList == null || mutableStoreList.Length == 0 || mutableStoreList[0] == null)
        {
            mutableStoreList = (StoreItemSO[])storeList.Clone();
        }
    }

    public StoreItemSO GetItem(int itemIndex)
    {
        
        if(itemIndex < mutableStoreList.Length)
        {
            return mutableStoreList[itemIndex];
        }
        else
        {
            return null;
        }
    }

    public void RemoveItem(int itemIndex)
    {
        StoreItemSO[] items = new StoreItemSO[mutableStoreList.Length - 1];
        for(int i = 0; i < items.Length; i++ )
        {
            if(i < itemIndex)
            {
                items[i] = mutableStoreList[i];
            } else
            {
                items[i] = mutableStoreList[i + 1];
            }
        }
        mutableStoreList = (StoreItemSO[])items.Clone();
    }
}
