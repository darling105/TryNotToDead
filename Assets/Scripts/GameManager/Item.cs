using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public PlayerWeaponManager weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDescription;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDescription = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case Enums.ItemType.Melee:
            case Enums.ItemType.Range:
                textDescription.text =string.Format(data.itemDescription, data.damages[level] * 100, data.counts[level]);
                break;
            case Enums.ItemType.Glove:
            case Enums.ItemType.Shoe:
                textDescription.text = string.Format(data.itemDescription, data.damages[level] * 100);
                break;
            default:
                textDescription.text = string.Format(data.itemDescription);
                break;
        }
        
    }
    
    public void OnClick()
    {
        switch (data.itemType)
        {
            case Enums.ItemType.Melee:
            case Enums.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<PlayerWeaponManager>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];
                    
                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case Enums.ItemType.Glove:
            case Enums.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case Enums.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
