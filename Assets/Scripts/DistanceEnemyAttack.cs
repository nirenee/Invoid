using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyAttack : MonoBehaviour
{
  
    public Transform attackspawner;
    public Transform playerposition;
    public float anglevision;
    public GameObject bullet;
    private float attackrange = 30;
    private float speedattack = 1f;


    private void Start()
    {
        StartCoroutine(Attacking());
    }
        
        


    private IEnumerator Attacking()
    {
        while (true) {
            canAttack();
            yield return new WaitForSeconds(speedattack);
        }
        
            
    }
  
    private void canAttack()
    {
        var distanceplayer = Vector3.Distance(playerposition.position, transform.position);

        
        if (distanceplayer <= attackrange && isFacingPlayer())
        {
            Vector3 direction = (playerposition.position - attackspawner.position).normalized;
            attackspawner.rotation = Quaternion.LookRotation(direction);
            Instantiate(bullet, attackspawner.position, attackspawner.rotation);
        } 
    }

   

    private bool isFacingPlayer()
    {
        Vector3 dirToPlayer = (playerposition.position - transform.position).normalized;
        var angleToplayer = Vector3.Angle(transform.forward, dirToPlayer);
        return angleToplayer < anglevision / 2;
    }
}
