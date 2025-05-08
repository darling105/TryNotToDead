using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isLive;
    public float gameTimer;
    public float maxGameTimer = 2 * 10f;

    [Header("Player Info")]
    public float health;
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
    public Result uiResult;
    public GameObject enemyCleaner;

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

    public void GameStart()
    {
        health = maxHealth;
        levelUp.Select(0);

        ResumeGame();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        StopGame();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryCoroutine());
    }

    private IEnumerator GameVictoryCoroutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        StopGame();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!isLive)
            return;

        gameTimer += Time.deltaTime;

        if (gameTimer > maxGameTimer)
        {
            gameTimer = maxGameTimer;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;
        
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
