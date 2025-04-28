using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager
{
    [SerializeField] private int moveSpeed = 4;
    [SerializeField] public Vector2 movementInput;

    [HideInInspector] public Scanner scanner;

    protected override void Awake()
    {
        base.Awake();
        scanner = GetComponent<Scanner>();
    }

    protected override void Update()
    {
        base.Update();
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 moveDir = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDir);
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        anim.SetFloat("isMoving", movementInput.magnitude);

        if (movementInput.x != 0)
            sr.flipX = movementInput.x < 0;
    }
}