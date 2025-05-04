using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Main Info")]
    public Enums.ItemType itemType;
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;
    [Header("Weapon")]
    public GameObject projectile;
}
