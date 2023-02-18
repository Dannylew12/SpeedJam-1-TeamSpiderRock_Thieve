using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    public enum STATUS { IDLE, PATROL, CHASE, RETURN, DEFAULT }
    public STATUS status = STATUS.DEFAULT;
    private NavMeshAgent meshAgent;
    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (status == STATUS.IDLE)
        {
            IdleBehavior();
            return;
        }
        if (status == STATUS.PATROL)
        {
            PatrolBehavior();
            return;
        }
        if (status == STATUS.CHASE)
        {
            ChaseBehavior();
            return;
        }
        if (status == STATUS.RETURN)
        {
            ReturnBehavior();
            return;
        }
    }

    private void IdleBehavior()
    {

    }

    [SerializeField] private List<Transform> patrolPath;
    private int pathIdx = 0;
    private void PatrolBehavior()
    {
        if (meshAgent.remainingDistance < .1f)
        {
            pathIdx = (pathIdx + 1) % patrolPath.Count;
        }
        meshAgent.SetDestination(patrolPath[pathIdx].position);
    }
    
    private void ChaseBehavior()
    {

    }

    private void ReturnBehavior()
    {

    }


}
