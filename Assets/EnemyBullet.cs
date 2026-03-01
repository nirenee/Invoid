using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet")]
    Rigidbody bulletrb;
    public float speedbullet;
    public float bullettime;
    public float bulletdamage;
    private float time;

    private void Awake()
    {
        
        bulletrb = GetComponent<Rigidbody>();
        bulletrb.velocity = this.transform.forward * speedbullet;
        if (bulletrb== null)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= bullettime)
        {
            Destroy(this.gameObject);
        }
    }
}
