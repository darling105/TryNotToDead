using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public float gameTimer;
    public float maxGameTimer = 2 * 10f;

    [Header("Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("Game Object")]
    public PoolManager pool;
    public PlayerManager player;

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
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer > maxGameTimer)
        {
            gameTimer = maxGameTimer;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}