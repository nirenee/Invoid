using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    private Transform playertransform;
    public Transform camerapivot;
    public Transform cameratransform;
    Vector3 cameraFollow = Vector3.zero;
    public float speedCamera= 0.2f;
    public float minimumhigh = -35;
    public float maximumhigh = 70;
    public float minangle = 45;
    public float maximumangle = 45;
    public float cameraCollisionRadius;
    public LayerMask collisionLayers;
    private Vector3 cameraVecPos;

    public float CameraCollisionoffset = 0.2f;
    public float minCollisionOffset = 0.2f;
    private float CameraUpDown;
    private float CameraLeftRight;
    private float defaultposition;
   

    public float cameraspeed = 2.0f;


    
    private void Awake()
    {
        playertransform = FindObjectOfType<playerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameratransform = Camera.main.transform;
        defaultposition = cameratransform.localPosition.z;
        
    }

  
   public void FollowPlayer()
    {
        Vector3 playerscope= playertransform.position + new Vector3(2f,0f,1f);
        Vector3 Targetposition = Vector3.SmoothDamp(transform.position, playerscope, ref cameraFollow, speedCamera);
        transform.position = playerscope;
    }

    public void Rotate()
    {
        CameraUpDown = CameraUpDown + (inputManager.cameraInput.x * speedCamera);
        CameraLeftRight = CameraLeftRight - ( inputManager.cameraInput.y * speedCamera);
        CameraLeftRight = Mathf.Clamp(CameraLeftRight, minimumhigh, maximumhigh);

       
        Vector3 rotation = Vector3.zero;
        rotation.y = CameraUpDown;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = CameraLeftRight;
        targetRotation = Quaternion.Euler(rotation);
        camerapivot.localRotation = targetRotation;

    }

        public  void HandleCollisions()
    {
        float targetPosition = defaultposition;
        RaycastHit hit;
        Vector3 direction = cameratransform.position - camerapivot.position;
        direction.Normalize();
        if(Physics.SphereCast(camerapivot.position, cameraCollisionRadius,direction, out hit, Mathf.Abs(targetPosition),collisionLayers)){

            float distance = Vector3.Distance(camerapivot.position, hit.point);
            targetPosition =  -(distance-CameraCollisionoffset);

        }

        if(Mathf.Abs (targetPosition) < minCollisionOffset)
        {
            targetPosition = targetPosition - minCollisionOffset;
            
        }
        cameraVecPos.z = Mathf.Lerp(cameratransform.localPosition.z, targetPosition, 0.2f);
        cameratransform.localPosition = cameraVecPos;
    }

}
