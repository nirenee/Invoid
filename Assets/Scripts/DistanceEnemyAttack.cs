using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistanceEnemyAttack : MonoBehaviour
{
  
    public Transform attackspawner;
    public float anglevision;
    public GameObject bullet;
    public float attackrange = 1;
    private float speedattack = 1f;
    private float rotationspeed = 5f;
    private bool canRotate = false;
    private NavMeshAgent agent;
    private WanderState wanderState;
    private Animator animator;
    private Transform playerposition;


    private void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if(playerObject != null)
        {
          playerposition = playerObject.transform;
        }
        else
        {
            Debug.Log("there is no player in the scene");
        }
            agent = GetComponent<NavMeshAgent>();
        wanderState = GetComponent<WanderState>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Attacking());

    }
    private void Update()
    {
        RotatesToplayer();
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
            if (wanderState != null && wanderState.enabled)
            {
                wanderState.enabled = false;
            }

            if (agent != null && agent.isOnNavMesh)
            {
                agent.isStopped = true;
            }
            animator.SetTrigger("Attack");
            canRotate = true;
            Vector3 playerpositionhead = playerposition.position + new Vector3(0, 1.5f, 0);
            Vector3 directionattack = (playerpositionhead - attackspawner.position).normalized;
            attackspawner.rotation = Quaternion.LookRotation(directionattack);
            Instantiate(bullet, attackspawner.position, attackspawner.rotation);
        }
        else
        {
            canRotate = false;

            if (agent != null)
            {
                agent.isStopped = false;
            }

            if (wanderState != null && !wanderState.enabled)
            {
                wanderState.enabled = true;
            }
        }
    }

   

    private void RotatesToplayer()
    {
        if (!canRotate)
        {
            return;
        }
        Vector3 direction = playerposition.position - transform.position;
        direction.y = 0f;
        Quaternion targetrotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, rotationspeed * Time.deltaTime);
    }
    private bool isFacingPlayer()
    {
        Vector3 dirToPlayer = (playerposition.position - transform.position).normalized;
        var angleToplayer = Vector3.Angle(transform.forward, dirToPlayer);
        return angleToplayer < anglevision;
    }
}
