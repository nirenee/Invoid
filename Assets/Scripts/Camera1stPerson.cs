using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Camera1stPerson: MonoBehaviour
{
    InputManager inputManager;
    private Transform playertransform;
    public Transform camerapivot;
    public Transform cameratransform;
    Vector3 cameraFollow = Vector3.zero;
    public Slider camspeed;
    public float slidervalue;
    public float speedCamera= 2f;
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
  

    private void Awake()
    {
        playertransform = FindObjectOfType<playerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameratransform = Camera.main.transform;
        defaultposition = cameratransform.localPosition.z;
        camspeed.value = speedCamera;
        
    }

    public void ChangeCameraSpeed(float value)
    {
        slidervalue = value;
        speedCamera = camspeed.value;
    }
  
   public void FollowPlayer()
    {
        Vector3 playerscope= playertransform.position;
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


}
