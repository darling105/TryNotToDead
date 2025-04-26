using System;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] public float moveSpeed = 3;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D rbTarget;

    [SerializeField] private bool isLive = true;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isLive)
            return;

        Vector2 moveDirection = rbTarget.position - rb.position;
        Vector2 nextDirection = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextDirection);
        rb.linearVelocity = Vector2.zero;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (!isLive)
            return;

        sr.flipX = rbTarget.position.x < rb.position.x;
    }

    private void OnEnable()
    {
        rbTarget = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        anim.runtimeAnimatorController = animatorControllers[spawnData.spriteType];
        moveSpeed = spawnData.moveSpeed;
        maxHealth = spawnData.health;
        health = spawnData.health;
    }
}