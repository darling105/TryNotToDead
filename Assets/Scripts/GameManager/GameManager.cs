using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isLive;
    public float gameTimer;
    public float maxGameTimer = 2 * 10f;

    [Header("Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp =
    {
        10, 30, 60, 100, 150, 210, 280, 360, 450, 600
    };

    [Header("Game Object")]
    public PoolManager pool;
    public PlayerManager player;
    public LevelUp levelUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = maxHealth;

        levelUp.Select(0);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isLive)
            return;

        gameTimer += Time.deltaTime;

        if (gameTimer > maxGameTimer)
        {
            gameTimer = maxGameTimer;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            levelUp.Show();
        }
    }

    public void StopGame()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
