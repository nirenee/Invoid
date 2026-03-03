using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyHealth : MonoBehaviour
{
    Bullet bullet;
    Health health;
    private Animator animator;
    [Range (0,100)] public float percentageloot = 50f;
    public GameObject objectdrop;

    public void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        if (health == null)
        {
            Debug.LogError("EnemyHealth: No Health component on this GameObject.", this);
            return;
        }     
        if (animator == null)
        {
            Debug.LogError("EnemyHealth: No Animator componen");
            return;
        }
     }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colision");

        if (other.CompareTag("PlayerAttack"))
        {
            var bullet = other.GetComponent<Bullet>();
            Debug.Log("collision");
            animator.SetTrigger("gethit");
            health.ApplyDamage(bullet.bulletdamage);
            if (health.currenthealth <= 0)
            {
                LootObject();
                var isDead = true;
                animator.SetBool("isDead", isDead);
                Destroy(this.gameObject);
            }



        }



    }

    private void LootObject()
    {
        
        float randomnum;
        randomnum = Random.Range(0, 100);
        Vector3 spawner = transform.position + new Vector3 (0,1,0);

        if(randomnum <= percentageloot)
        {
            Instantiate(objectdrop,spawner, Quaternion.identity);
        }
    }
    

}
