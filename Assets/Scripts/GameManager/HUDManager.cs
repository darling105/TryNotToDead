using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Enums.InfoType infoType;
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
            case Enums.InfoType.Exp:
                float currentExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length -1)];
                mySlider.value = currentExp / maxExp;
                break;
            case Enums.InfoType.Level:
                myText.text = string.Format("Lvl.{0:F0}", GameManager.instance.level);
                break;
            case Enums.InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case Enums.InfoType.Time:
                float remainingTime = GameManager.instance.maxGameTimer - GameManager.instance.gameTimer;
                int min = Mathf.FloorToInt(remainingTime / 60);
                int sec = Mathf.FloorToInt(remainingTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case Enums.InfoType.Health:
                float currentHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = currentHealth / maxHealth;
                break;
        }
    }
}