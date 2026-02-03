using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : MonoBehaviour
{
    [Header("References")]
    public Transform[] patrolPoints;
    public float distancestop;

    private NavMeshAgent agent;
    private int indexwaypoints;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            NextWaypoint();
        }
        else
        {
            Debug.LogWarning("StatePatrol: no hay puntos de patrulla asignados.", this);
        }
    }

    private void Update()
    {
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

    private void UpdateAnim()
    {
        var isWalking = agent.velocity.sqrMagnitude > 0.01;
        animator.SetBool("isWalking", isWalking);
    }
}
