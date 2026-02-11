using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    Rigidbody bulletrb;
    public float speedbullet;
    public float bullettime;
    public float bulletdamage;
    private float time;

    private void Awake()
    {
        //Get component can return a null, do you want to crash in this case?
        bulletrb = GetComponent<Rigidbody>();
        bulletrb.velocity = this.transform.forward * speedbullet;
    }
    //Empty lines?



    private void OnTriggerEnter(Collider other)
    {
        //try to avoid submitting commented code
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
