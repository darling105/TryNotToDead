using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTimer;
    public float maxGameTimer = 2 * 10f;

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
}