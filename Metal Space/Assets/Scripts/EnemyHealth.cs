using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyHealth : MonoBehaviour
{
    Bullet bullet;
    Health health;
    private Animator animator;

   

    public void Awake()
    {
        bullet = FindObjectOfType<Bullet>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colision");

        if (other.CompareTag("PlayerAttack"))
        {

            animator.SetTrigger("gethit");
            health.ApplyDamage(bullet.bulletdamage);
            if (health.currenthealth <= 0)
            {
                var isDead = true;
                animator.SetBool("isDead", isDead);
                Destroy(this.gameObject);
            }



        }



    }
    

}
