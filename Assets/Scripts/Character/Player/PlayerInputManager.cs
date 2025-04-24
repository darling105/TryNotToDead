using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    public PlayerManager player;

    [HideInInspector] private Vector2 movementInput;
    private float horizontalInput;
    private float verticalInput;
    private float moveAmount;

    private void Start()
    {
        if (playerControls == null)
            playerControls.Disable();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }
    
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleAllInput()
    {
        
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }
}