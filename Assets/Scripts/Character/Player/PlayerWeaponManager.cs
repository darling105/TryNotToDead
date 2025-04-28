using System;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [HideInInspector] PlayerManager player;

    public int weaponID;
    public int prefabsID;
    public float damage;
    public int count;
    public float speed;

    private float timer;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (weaponID)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }

                break;
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (weaponID == 0)
            Batch();
    }

    private void Init()
    {
        switch (weaponID)
        {
            case 0:
                speed = 150;
                Batch();

                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.GetGameObject(prefabsID).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotationVector = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotationVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPosition = player.scanner.nearestTarget.position;
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;
        Transform bullet = GameManager.instance.pool.GetGameObject(prefabsID).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        bullet.GetComponent<Bullet>().Init(damage, count, direction);
    }
}