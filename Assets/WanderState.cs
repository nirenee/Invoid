using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState: MonoBehaviour
{
    private NavMeshAgent agent;
    public float wanderarea;
    public float wanderstop;
    public float waitTime;
    private Coroutine wanderCoroutine;



    private void OnEnable()
    {
        if (wanderCoroutine == null)
        {
            wanderCoroutine = StartCoroutine(WanderingAround());
        }
    }

    private void OnDisable()
    {
        if (wanderCoroutine != null)
        {
            StopCoroutine(wanderCoroutine);
            wanderCoroutine = null;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private IEnumerator WanderingAround()
    {
        while (true)
        {
            if (agent == null || !agent.isOnNavMesh)
            {
                yield return null;
                continue;
            }
            Vector3 nextPosition;
            if(TryRandomPoint(transform.position,wanderarea,out nextPosition))
            {
                agent.isStopped = false;
                agent.SetDestination(nextPosition);
            
                while(!agent.pathPending && agent.remainingDistance > wanderstop)
                {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
       
    }



    private bool TryRandomPoint(Vector3 center, float radius, out Vector3 resultpoint)
    {
        for (int i = 0; i <= 20; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = center.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 2f, NavMesh.AllAreas))
            {
                resultpoint = hit.position;
                return true;
            }

        }
        resultpoint = center;
        return false;
    }
   
}
