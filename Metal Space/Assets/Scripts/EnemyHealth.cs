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
        //What if any of these are null?
        bullet = FindObjectOfType<Bullet>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    //Other is a strange arg name, how about colidedObject?
    private void OnTriggerEnter(Collider other)
    {
        //Empty lines? So many empty lines ... :D
        
        
        Debug.Log("colision");

        //Consider moving hardcoded values to globals at the top or moving them to a seperate file, Eg. Player.cs?
        if (other.CompareTag("PlayerAttack"))
        {

            animator.SetTrigger("gethit");
            health.ApplyDamage(bullet.bulletdamage);
            if (health.currenthealth <= 0)
            {
                LootObject();
                var isDead = true;
                //Consider writing a wrapper around Animator class that has all your hardcoded states, then the interface could be animator.SetDied() and in case of refactoring you know that all hardcoded stuff is in animator classes
                animator.SetBool("isDead", isDead);
                Destroy(this.gameObject);
            }



        }



    }

    private void LootObject()
    {
        //float randomnum = Random.Range(0, 100); cleaner?
        float randomnum;
        randomnum = Random.Range(0, 100);

        if(randomnum <= percentageloot)
        {
            Instantiate(objectdrop,transform.position, Quaternion.identity);
        }
    }
    

}
