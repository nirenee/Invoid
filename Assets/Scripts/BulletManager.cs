using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [Header("Bullet")]
    public Transform bulletposini;
    public GameObject bulletprefab;

    public void HandleBullet()
    {
        if(bulletprefab == null || bulletposini == null)
        {
            Debug.Log("no");
            return;
        }
       
        RaycastHit camerahit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out camerahit,1f))
        {
            Vector3 shootDirection = (camerahit.point - bulletposini.position).normalized;
            bulletposini.rotation = Quaternion.LookRotation(shootDirection);
            Instantiate(bulletprefab, bulletposini.position, bulletposini.rotation);
            Debug.Log("si");
        }      
    }

}
