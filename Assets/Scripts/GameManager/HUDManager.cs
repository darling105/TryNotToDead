using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health
    }

    public InfoType infoType;
    private Text myText;
    private Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.Exp:
                float currentExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = currentExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lvl.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainingTime = GameManager.instance.maxGameTimer - GameManager.instance.gameTimer;
                int min = Mathf.FloorToInt(remainingTime / 60);
                int sec = Mathf.FloorToInt(remainingTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float currentHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = currentHealth / maxHealth;
                break;
        }
    }
}