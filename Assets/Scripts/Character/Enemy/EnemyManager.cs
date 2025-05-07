using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] public float moveSpeed = 3;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D rbTarget;
    private Collider2D cld;
    private WaitForFixedUpdate wait;

    [SerializeField] private bool isLive = true;

    protected override void Awake()
    {
        base.Awake();
        cld = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!GameManager.instance.isLive)
            return;
        
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 moveDirection = rbTarget.position - rb.position;
        Vector2 nextDirection = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextDirection);
        rb.linearVelocity = Vector2.zero;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (!GameManager.instance.isLive)
            return;
        
        if (!isLive)
            return;

        sr.flipX = rbTarget.position.x < rb.position.x;
    }

    private void OnEnable()
    {
        rbTarget = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        cld.enabled = true;
        rb.simulated = true;
        sr.sortingOrder = 2;
        anim.SetBool("isDead", false);
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        anim.runtimeAnimatorController = animatorControllers[spawnData.spriteType];
        moveSpeed = spawnData.moveSpeed;
        maxHealth = spawnData.health;
        health = spawnData.health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !isLive)
            return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            cld.enabled = false;
            rb.simulated = false;
            sr.sortingOrder = 1;
            anim.SetBool("isDead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    private IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 directionVector = transform.position - playerPosition;
        rb.AddForce(directionVector.normalized * 3, ForceMode2D.Impulse);
    }

    protected override void Dead()
    {
        base.Dead();
        gameObject.SetActive(false);
    }
}