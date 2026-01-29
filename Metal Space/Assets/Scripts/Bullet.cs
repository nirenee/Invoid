using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    Rigidbody bulletrb;
    public float speedbullet;
    public float bullettime;
    private float time;

    private void Awake()
    {
        bulletrb = GetComponent<Rigidbody>();
        bulletrb.velocity = this.transform.forward * speedbullet;
    }



    private void OnTriggerEnter(Collider other)
    {
        // Destroy(Instantiate(bulletrb.position, bulletrb.rotation,))
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
