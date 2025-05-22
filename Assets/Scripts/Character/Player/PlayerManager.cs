using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager
{
    [SerializeField] public float moveSpeed = 4;
    [SerializeField] public Vector2 movementInput;
    [SerializeField] public Hand[] hands;
    [SerializeField] public RuntimeAnimatorController[] animatorControllers;

    [HideInInspector] public Scanner scanner;


    protected override void Awake()
    {
        base.Awake();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()
    {
        moveSpeed *= CharacterStats.Speed;
        anim.runtimeAnimatorController = animatorControllers[GameManager.instance.playerID];
    }

    protected override void Update()
    {
        base.Update();

        if (!GameManager.instance.isLive)
            return;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!GameManager.instance.isLive)
            return;

        Vector2 moveDir = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDir);
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("isMoving", movementInput.magnitude);

        if (movementInput.x != 0)
            sr.flipX = movementInput.x < 0;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            anim.SetTrigger("isDead");
            GameManager.instance.GameOver();
        }
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
