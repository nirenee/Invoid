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
    //Empty lines?







    public void HandleBullet()
    {
        //I would prefer that you express your intention in code by writing
        //bool isValid = bulletprefab == null || bulletposini == null
        if(bulletprefab == null || bulletposini == null)
        {
            return;
        }
       
        RaycastHit camerahit;
        //Consider making ray hit distance a global or part of consts in another file, that should make it easier to refactor later
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out camerahit,1000f/*consider naming args if you write them like this*/))
        {
            Vector3 shootDirection = (camerahit.point - bulletposini.position).normalized;
            bulletposini.rotation = Quaternion.LookRotation(shootDirection);

            Instantiate(bulletprefab, bulletposini.position, bulletposini.rotation);
            //Im not sure what you want to do in this code. A few named variables would go a long way
        }
        //empty line?
      
    }
    //empty lines?
    //Did you consider using a linter?

   
}
