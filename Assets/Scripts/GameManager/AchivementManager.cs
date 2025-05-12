using System;
using System.Collections;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    [SerializeField] private Enums.Achivement[] achivements;
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achivements = (Enums.Achivement[])Enum.GetValues(typeof(Enums.Achivement));
        wait = new WaitForSecondsRealtime(3);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Enums.Achivement achivement in achivements)
        {
            PlayerPrefs.SetInt(achivement.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    private void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achivementName = achivements[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achivementName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (Enums.Achivement achivement in achivements)
        {
            CheckAchivement(achivement);
        }
    }

    private void CheckAchivement(Enums.Achivement achivement)
    {
        bool isAchivement = false;

        switch (achivement)
        {
            case Enums.Achivement.UnlockJimmy:
                isAchivement = GameManager.instance.kill >= 100;
                break;
            case Enums.Achivement.UnlockLily:
                isAchivement = GameManager.instance.gameTimer == GameManager.instance.maxGameTimer;
                break;
        }

        if (isAchivement && PlayerPrefs.GetInt(achivement.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achivement.ToString(), 1);

            for (int i = 0; i < uiNotice.transform.childCount; i++)
            {
                bool isActive = i == (int)achivement;
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
            }
            
            StartCoroutine(NoticeRoutine());
        }
    }

    private IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        SoundManager.instance.PlaySFX(Enums.Sfx.LevelUp);

        yield return wait;
        uiNotice.SetActive(false);
    }
}
