using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;

    PlayerController playerController;
    Vector3 currentPlayerPosition;
    Vector3 realWaypoint;
    public bool followPlayer = false;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        playerController = FindAnyObjectByType<PlayerController>();
        realWaypoint = waypoints[2].position;
    }

    void Update()
    {
        currentPlayerPosition = playerController.transform.position;
        

        if (followPlayer)
        {
            waypoints[2].position = currentPlayerPosition;
            navMeshAgent.SetDestination(waypoints[2].position);
        }

        else
        {
            waypoints[1].position = realWaypoint;
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        }



    }
}
