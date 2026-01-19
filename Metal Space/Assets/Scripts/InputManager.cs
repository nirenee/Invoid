using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerControls;
    AnimationManager animatormanager;
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
   
    private float amountmovement;


    private void Awake()
    {
        animatormanager = GetComponent<AnimationManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerController();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        amountmovement = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatormanager.UpdateAnimator(horizontalInput, amountmovement);
    }
}