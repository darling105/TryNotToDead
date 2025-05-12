using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal,
    }

    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health,
    }

    public enum Achivement
    {
        UnlockJimmy,
        UnlockLily,
    }

    public enum Sfx
    {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Win,
    }
}
