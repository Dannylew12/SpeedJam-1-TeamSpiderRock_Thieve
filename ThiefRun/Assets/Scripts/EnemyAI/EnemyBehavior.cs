using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    public enum STATUS { IDLE, PATROL, CHASE, RETURN, DEFAULT }

    public STATUS curStatus = STATUS.DEFAULT;
    private STATUS originalStatus;
    private NavMeshAgent meshAgent;
    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        originalStatus = curStatus;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (curStatus == STATUS.IDLE)
        {
            IdleBehavior();
            return;
        }
        if (curStatus == STATUS.PATROL)
        {
            PatrolBehavior();
            return;
        }
        if (curStatus == STATUS.CHASE)
        {
            ChaseBehavior();
            return;
        }
        if (curStatus == STATUS.RETURN)
        {
            ReturnBehavior();
            return;
        }
    }

    [SerializeField] private float maxTurnAngle;
    [SerializeField] private float turnSpeed;
    private bool clockwise = true;
    private float curAngle = 0f;
    private void IdleBehavior()
    {
        float sign = clockwise ? 1 : -1;
        float prevAngle = curAngle;
        curAngle = Mathf.Clamp(curAngle + Time.deltaTime * turnSpeed * sign, -maxTurnAngle, maxTurnAngle);
        transform.Rotate(0, curAngle - prevAngle, 0);
        if (curAngle >= maxTurnAngle || curAngle <= -maxTurnAngle)
            clockwise = !clockwise;
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

    public Transform player;
    private void ChaseBehavior()
    {
        meshAgent.speed = 6;
        meshAgent.angularSpeed = 180;
        meshAgent.SetDestination(player.position);
    }

    private void ReturnBehavior()
    {

    }


}
