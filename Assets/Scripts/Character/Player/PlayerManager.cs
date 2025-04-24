using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider collider;
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Animator anim;
    [SerializeField] private int moveSpeed = 4;
    [SerializeField] private Vector2 movementInput;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    protected override void FixedUpdate()
    {
        Vector2 moveDir = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDir);
    }

    protected override void LateUpdate()
    {
        anim.SetFloat("isMoving", movementInput.magnitude);
        
        if (movementInput.x != 0)
            sr.flipX = movementInput.x < 0;
    }
}