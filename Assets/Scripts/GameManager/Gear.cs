using UnityEngine;

public class Gear : MonoBehaviour
{
    public Enums.ItemType itemType;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemID;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        itemType = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    private void ApplyGear()
    {
        switch (itemType)
        {
            case Enums.ItemType.Glove:
                RateUp();
                break;
            case Enums.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    private void RateUp()
    {
        PlayerWeaponManager[] weapons = transform.parent.GetComponentsInChildren<PlayerWeaponManager>();

        foreach (PlayerWeaponManager weapon in weapons)
        {
            switch (weapon.weaponID)
            {
                case 0:
                    float speed = 150 * CharacterStats.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;
                default:
                    speed = 0.5f * CharacterStats.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }

    private void SpeedUp()
    {
        float speed = 4 * CharacterStats.Speed;
        GameManager.instance.player.moveSpeed = speed + speed * rate;
    }
}
