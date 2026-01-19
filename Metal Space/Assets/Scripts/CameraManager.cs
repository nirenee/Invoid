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
    Vector3 cameraFollow = Vector3.zero;
    public float speedCamera= 0.2f;

    private float CameraUpDown;
    private float CameraLeftRight;

    public float cameraspeed = 2.0f;


    
    private void Awake()
    {
        playertransform = FindObjectOfType<playerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        
    }

  
   public void FollowPlayer()
    {
        Vector3 Targetposition = Vector3.SmoothDamp(transform.position,playertransform.position, ref cameraFollow, speedCamera);
        transform.position = playertransform.position;
    }

    public void Rotate()
    {
        CameraUpDown = CameraUpDown + (inputManager.cameraInput.x * speedCamera);
        CameraLeftRight = CameraLeftRight - ( inputManager.cameraInput.y * speedCamera);

        Vector3 rotation = Vector3.zero;
        rotation.y = CameraUpDown;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = CameraLeftRight;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        camerapivot.localRotation = targetRotation;

    }
}
