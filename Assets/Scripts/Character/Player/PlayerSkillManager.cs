using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour
{
    public int prefabsID;
    
    [Header("Skill")]
    [SerializeField] private int bulletCount = 36;
    [SerializeField] private float ultraDamage = 100f;
    [SerializeField] private float cooldownSkillTime = 10f;

    [Header("UI REFERENCES")]
    [SerializeField] private Button skillButton;
    [SerializeField] private Image skillCooldownOverlay;
    [SerializeField] private Image skillIcon;

    private bool isCooldown = false;
    private float currentCooldown;

    void Start()
    {
        skillButton.onClick.AddListener(ActivateSkill);
        skillCooldownOverlay.fillAmount = 0;
    }

    void Update()
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            skillCooldownOverlay.fillAmount = currentCooldown / cooldownSkillTime;

            if (currentCooldown <= 0)
            {
                isCooldown = false;
                skillCooldownOverlay.fillAmount = 0;
                skillButton.interactable = true;
            }
        }
    }

    public void ActivateSkill()
    {
        if (isCooldown)
            return;
        
        FireUltraBullets();
        StartCoolDown();
    }

    private void FireUltraBullets()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            direction.Normalize();

            Transform bullet = GameManager.instance.pool.GetGameObject(prefabsID).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            bullet.GetComponent<Bullet>().Init(ultraDamage, 0, direction);
        }

        SoundManager.instance.PlaySFX(Enums.Sfx.Range);
    }

    private void StartCoolDown()
    {
        isCooldown = true;
        currentCooldown = cooldownSkillTime;
        skillButton.interactable = false;
        skillCooldownOverlay.fillAmount = 1;
    }
}
