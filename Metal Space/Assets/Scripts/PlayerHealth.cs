using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  Health health;
    public float damageReceived;
    private int enemycounter;
    private Coroutine damagecoroutine;
    
  
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("deathwall"))
        {
            Destroy(this.gameObject);

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("colliison");
            enemycounter++;
            damagecoroutine = StartCoroutine(DamageOverTime());
            

        }
      
     

    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Debug.Log("no colliison");
        enemycounter = Mathf.Max(0, enemycounter - 1);
        if(enemycounter == 0)
        {
            StopCoroutine(DamageOverTime());
            damagecoroutine = null;
        }
        
    }

    private IEnumerator DamageOverTime()
    {
        WaitForSeconds wait = new WaitForSeconds(Mathf.Max(0.01f, 0.5f));
    
        while(enemycounter > 0 && health.currenthealth > 0)
        {
            health.ApplyDamage(damageReceived);
            yield return wait;
        }
        if (health.currenthealth <= 0)
        {
            Destroy(this.gameObject);
        }
        damagecoroutine = null;
     }
   
}
