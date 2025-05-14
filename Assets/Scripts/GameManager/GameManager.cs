using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isLive;
    public float gameTimer;
    public float maxGameTimer = 2 * 10f;

    [Header("Player Info")]
    public int playerID;
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
    public Transform uiJoystick;
    public GameObject enemyCleaner;
    public GameObject characterSelection;

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
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id)
    {
        playerID = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        levelUp.Select(playerID % 2);
        ResumeGame();
        SoundManager.instance.PlayBGM(true);
        SoundManager.instance.PlaySFX(Enums.Sfx.Select);
    }

    public void CharacterSelection()
    {
        SoundManager.instance.PlaySFX(Enums.Sfx.Select);
        characterSelection.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
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
        
        SoundManager.instance.PlayBGM(false);
        SoundManager.instance.PlaySFX(Enums.Sfx.Lose);
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
        
        SoundManager.instance.PlayBGM(false);
        SoundManager.instance.PlaySFX(Enums.Sfx.Win);
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
        uiJoystick.localScale = Vector3.zero;
    }

    public void ResumeGame()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoystick.localScale = Vector3.one;
    }
}
