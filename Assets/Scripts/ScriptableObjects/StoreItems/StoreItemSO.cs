using UnityEngine;

[CreateAssetMenu(fileName = "StoreItem", menuName = "Store/Item")]
public class StoreItemSO : ScriptableObject
{
    [SerializeField] private StoreItemType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int value;

    [Header("Power ups")]
    [SerializeField] private int cannonNumber;

    public Sprite Sprite { get => sprite; }
    public int Value { get => value; }

    public StoreItemType ItemType { get => type; }
    public int CannonNumber { get => cannonNumber; }
}
