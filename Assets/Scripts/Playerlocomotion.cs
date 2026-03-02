using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlocomotion : MonoBehaviour
{
    AnimationManager animationman;
    playerManager playerManager;
    InputManager inputmanager;
    Vector3 directionmove;
    Transform camera;
    Rigidbody PlayerRB;


    [Header("Falling and landing")]
    public bool isOnGround;
    public float inAirTimer;
    public float fallinSpeed;
    public float leapingspeed;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask ground;

    [Header("Dashing")]
    public bool isDashing;
    public float dashvelocity = 30f;
    public float dashtime = 0.3f;
    public float dashcooldown = 1f;
    private float lastDashTime = -Mathf.Infinity;

    public TrailRenderer tr;

    [Header("Jumping")]
    public bool isJumping;
    public float speedjumping;
    public float jumpHeight = 3;
    public float gravityIntensity = -15;


    public float moveSpeed = 7;
    public float rotationSpeed;
 
    private void Awake()
    {

        tr.emitting = false;
        inputmanager = GetComponent<InputManager>();
        PlayerRB = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
        animationman = GetComponent<AnimationManager>();
        playerManager = FindObjectOfType<playerManager>();
    }
    public void HandleAllMovement()
    {
        HandleLanding();
        if (playerManager.isInteracting && isOnGround)
        {
            return;
        }
        
        HandleMovement();
        HandleRotation();

    }



    public void HandleDash()
    {
        if (Time.time >= lastDashTime + dashcooldown && !isDashing)
        {
            StartCoroutine(Dash());
        }
       
    }

    private void HandleMovement()
    {

         if (isDashing){
          return;   
         }
        
        directionmove = camera.forward * inputmanager.verticalInput;
        directionmove = directionmove + camera.right * inputmanager.horizontalInput;
        directionmove.Normalize();
        directionmove.y = 0;
        directionmove = directionmove * moveSpeed;

        Vector3 movementVelocity = directionmove;
        movementVelocity.y =PlayerRB.velocity.y;
        PlayerRB.velocity = movementVelocity;
      
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        if (inputmanager.attack_button)
        {
            targetDirection = camera.forward;
            targetDirection.y = 0f;

        }
        else
        {

            targetDirection = camera.forward * inputmanager.verticalInput;
            targetDirection = targetDirection + camera.right * inputmanager.horizontalInput;
          
            targetDirection.y = 0;

        }


        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed * Time.deltaTime);
   
        transform.rotation = playerRotation;
    
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        tr.emitting = true;
      
        Vector3 dashDirection = transform.forward;

        float startTime = Time.time;

        while (Time.time < startTime + dashtime)
        {
            PlayerRB.velocity = dashDirection * dashvelocity;
            yield return null;
        }
        tr.emitting = false;
        isDashing = false;
       
    }

    private void HandleLanding()
    {
        RaycastHit ray;
        Vector3 raycastingground = transform.position;
        raycastingground.y = raycastingground.y + rayCastHeightOffSet;
        if (!isOnGround && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animationman.PlayTargetAnimation("Falling Idle", true);

            }
            inAirTimer = inAirTimer + Time.deltaTime;
           PlayerRB.AddForce(transform.forward * leapingspeed);
           PlayerRB.AddForce(-Vector3.up * fallinSpeed);
        }

        if (Physics.SphereCast(raycastingground, 0.1f, -Vector3.up, out ray,ground))
        {
            if (!isOnGround && !playerManager.isInteracting)
            {
                animationman.PlayTargetAnimation("Falling to Landing", true);
            }
            inAirTimer = 0;
            isOnGround = true;
            isJumping = false;
        }
        else
        {
            isOnGround = false;
        }
    }
    public void HandleJumping()
    {
        if (isOnGround)
        {
            animationman.animator.SetBool("isJumping",true);
            animationman.PlayTargetAnimation("Jumping", false);
            
            speedjumping = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = PlayerRB.velocity;
            playerVelocity.y = speedjumping;
            PlayerRB.velocity = playerVelocity;
            isOnGround = false;
        }

    }



}
