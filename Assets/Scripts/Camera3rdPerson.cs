using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraManager: MonoBehaviour
{
    InputManager inputManager;
    private Transform playertransform;
    public Transform camerapivot;
    public Transform cameratransform;
    Vector3 cameraFollow = Vector3.zero;
    public Slider camspeed;
    public float slidervalue;
    public float speedCamera= 360f;
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
        cameratransform.position = camerapivot.position;
        cameratransform.rotation = camerapivot.rotation;
    }

    public void ChangeCameraSpeed(float value)
    {
        slidervalue = value;
        speedCamera = camspeed.value;
    }
  
   public void FollowPlayer()
    {
        cameratransform.position = camerapivot.position;
    }

    public void Rotate()
    {
        CameraUpDown = CameraUpDown + (inputManager.cameraInput.x * speedCamera * Time.deltaTime);
        CameraLeftRight = CameraLeftRight - (inputManager.cameraInput.y * speedCamera * Time.deltaTime);
        CameraLeftRight = Mathf.Clamp(CameraLeftRight, minimumhigh, maximumhigh);

        Quaternion targetRotation = Quaternion.Euler(CameraLeftRight, CameraUpDown, 0f);
        camerapivot.rotation = targetRotation;
        cameratransform.rotation = camerapivot.rotation;
    }

    public  void HandleCollisions()
    {
        float targetPosition = defaultposition;
        RaycastHit hit;
        Vector3 direction = camerapivot.TransformDirection(Vector3.back);

        if (Physics.SphereCast(camerapivot.position, cameraCollisionRadius, direction,
                                out hit, targetPosition, collisionLayers))
        {
            float distance = Vector3.Distance(camerapivot.position, hit.point);
            targetPosition = Mathf.Max(minCollisionOffset, distance - CameraCollisionoffset);
        }

        cameraVecPos.z = Mathf.Lerp(cameratransform.localPosition.z, targetPosition, 0.2f);
        cameratransform.localPosition = cameraVecPos;
    }

}
