using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [Header("Bullet")]
    Rigidbody bulletrb;
    public Transform bulletposini;
    public bool isShooting = false;
    public GameObject bulletprefab;







    public void HandleBullet()
    {
        if(bulletprefab == null || bulletposini == null)
        {
            return;
        }
       
        RaycastHit camerahit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out camerahit,1000f))
        {
            Vector3 shootDirection = (camerahit.point - bulletposini.position).normalized;
            bulletposini.rotation = Quaternion.LookRotation(shootDirection);

            Instantiate(bulletprefab, bulletposini.position, bulletposini.rotation);
        }
      
    }

   
}
