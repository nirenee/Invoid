using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

enum EnumState
{
    Patrolling,
    Chasing,
    Attacking
    }
public class StatePatrol : MonoBehaviour
{
    [Header("References")]
    public Transform[] patrolPoints;
    public float distancestop;
    public Transform playerposition;
    public float enemyvisiondistance;
    public float enemyvisionangle;
    public float loseplayer;


    private NavMeshAgent agent;
    private int indexwaypoints;
    private Animator animator;
    private EnumState currentstate;
    private float timewhenloseplayer = 2;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentstate = EnumState.Patrolling;
            NextWaypoint();
        }
      
    }

    private void Update()
    {
        var distanceplayer =   Vector3.Distance(playerposition.position, transform.position);
        if(distanceplayer == null) {
            return;
        }
        switch (currentstate)
        {
            case EnumState.Patrolling:
                Patrol();
                if(distanceplayer <= enemyvisiondistance)
                {
                    currentstate = EnumState.Chasing;
                }
             break;
            case EnumState.Chasing:
                Chasing();
                if(distanceplayer > enemyvisionangle)
                {
                    currentstate = EnumState.Patrolling;
                }
             break;
        }
       
      
        Patrol();
        UpdateAnim();
    }
    private IEnumerator PatrolPointWait()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.1f);

        NextWaypoint();
        agent.isStopped = false;

    }
    private void NextWaypoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            return;
        }
        agent.SetDestination(patrolPoints[indexwaypoints].position);
        indexwaypoints = (indexwaypoints + 1) % patrolPoints.Length;
    }
    private void Patrol()
    {
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }


        if (!agent.pathPending && agent.remainingDistance <= distancestop)
        {
            StartCoroutine(PatrolPointWait());
        }

    }

    private void Chasing()
    {
        agent.SetDestination(playerposition.position);
        if (!CanSeePlayer())
        {
            timewhenloseplayer += Time.deltaTime;
            if (timewhenloseplayer > loseplayer)
            {
               
               currentstate = EnumState.Patrolling;
                ClosestPointCommand();
            }

        }
    }

    private void UpdateAnim()
    {
        var isWalking = agent.velocity.sqrMagnitude > 0.01;
        animator.SetBool("isWalking", isWalking);
    }

    private bool CanSeePlayer()
    {
        return isFacingplayer() && PlayerisBlocked();
    }

    private bool isFacingplayer()
    {
        Vector3 dirToPlayer = ( playerposition.position - transform.position).normalized;
        var angleToplayer = Vector3.Angle(transform.forward, dirToPlayer);
        return angleToplayer < enemyvisionangle / 2;
    }
    private bool PlayerisBlocked()
    {
        RaycastHit hit;
        Vector3 dirToPlayer = (playerposition.position - transform.position).normalized;
        if(Physics.Raycast(transform.position, dirToPlayer.normalized, out hit, dirToPlayer.magnitude))
        {
            return hit.transform == playerposition;
        }
        return true;

    }

    private void ClosestPointCommand()
    {
        if(patrolPoints.Length == 0)
        {
            return;
        }

        var closestPoint = float.MaxValue;
        var closestindex = 0;

        for (int i = 0; i < patrolPoints.Length; i++) {
            var distance = Vector3.Distance(transform.position, patrolPoints[i].position);

            if (distance < closestPoint)
            {
                closestPoint = distance;
                 closestindex = 1;
            }

        }
        indexwaypoints = closestindex;
        agent.SetDestination(patrolPoints[indexwaypoints].position);
    }
}
