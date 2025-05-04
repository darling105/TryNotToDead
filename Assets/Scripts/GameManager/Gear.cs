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
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    private void SpeedUp()
    {
        float speed = 4;
        GameManager.instance.player.moveSpeed = speed + speed * rate;
    }
}
