// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CarPathMovement : MonoBehaviour
{
    public GameObject pointsParent;
    public List<Transform> points;

    [Range(0.2f, 1.0f)]
    public float distanceToPoint = 0.2f;

    public bool resetOnLastWaypoint = false;

    private int destPoint = 0;
    private NavMeshAgent agent;

    private int collisions = 0;

    public CarCollisionDetection frontDetection;
    public CarCollisionDetection midDetection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (pointsParent != null)
        {
            foreach (Transform child in pointsParent.transform)
            {
                this.points.Add(child);
            }
        }


        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        if (resetOnLastWaypoint && destPoint == points.Count)
        {
            Destroy(gameObject);
            Destroy(this);
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint++;

       
    }

    void Update()
    {
        if (frontDetection.isColliding)
        {
            agent.speed = 0;
        } else
        {
            agent.speed = 5f;
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < distanceToPoint)
            GotoNextPoint();
    }
}