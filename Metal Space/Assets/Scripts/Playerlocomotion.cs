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

    public TrailRenderer tr;
    public float moveSpeed = 7;
    public float rotationSpeed;
 
   
    private float lastDashTime = -Mathf.Infinity;



   

    private void Awake()
    {

        tr.emitting = false;
        inputmanager = GetComponent<InputManager>();
        PlayerRB = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
        animationman = GetComponent<AnimationManager>();
        playerManager = GetComponent<playerManager>();
    }
    public void HandleAllMovement()
    {

        HandleLanding();
        if (playerManager.isInteracting){
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
        PlayerRB.velocity =  movementVelocity;
      
    }

  

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection= camera.forward * inputmanager.verticalInput;
        targetDirection = targetDirection + camera.right * inputmanager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
       
        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

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
        if (!isOnGround)
        {
            if (!playerManager.isInteracting)
            {
                animationman.PlayTargetAnimation("Falling Idle", true);

            }
            inAirTimer = inAirTimer + Time.deltaTime;
            PlayerRB.AddForce(transform.forward * leapingspeed);
            PlayerRB.AddForce(-Vector3.up * fallinSpeed);
        }

        if (Physics.SphereCast(raycastingground, 0.01f, -Vector3.up, out ray,ground))
        {
            if (!isOnGround && !playerManager.isInteracting)
            {
                animationman.PlayTargetAnimation("Falling to Landing", true);
            }
            inAirTimer = 0;
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        

    }
    public void HandleJumping()
    {

    }



}
