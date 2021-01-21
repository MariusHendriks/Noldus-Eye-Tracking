// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PathMovement : MonoBehaviour
{
    public GameObject pointsParent;
    public List<Transform> points;

    [Range(0.2f, 1.0f)]
    public float distanceToPoint = 0.2f;


    //[Range(1.0f, 2.0f)]
    [Range(1.0f, 10.0f)]
    public float movementSpeed = 1f;

    [Range(-1.0f, 1.0f)]
    public float happiness;

    public bool resetOnLastWaypoint = true;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private bool didStopAnimation = false;
    private bool restartAnimation = false;
    public bool waitingForCrossing = false;

    public Vector3 destinationOfAgent;

    public GameObject debugPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (pointsParent != null)
        {
            foreach (Transform child in pointsParent.transform)
            {
                this.points.Add(child);
            }
        }
        animator.SetBool("isMoving", true);


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
            agent.Warp(this.points[0].transform.position);
            destPoint = 0;
        }

        // Set the agent to go to the currently selected destination.

        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint++;


    }

    void Update()
    {
        if (waitingForCrossing)
        {
            if (!didStopAnimation)
            {
                didStopAnimation = true;
                restartAnimation = false;
                animator.SetBool("isMoving", false);
                agent.speed = 0f;
            }
            return;
        }

        if (!restartAnimation)
        {
            animator.SetBool("isMoving", true);
            didStopAnimation = false;
            restartAnimation = true;
        }

        agent.speed = 0.75f * Mathf.Pow(movementSpeed, 2) + 0.75f * movementSpeed;

        if (happiness < 0)
        {
            //Walk animations of sad are slower
            agent.speed += 0.7f * happiness * (1 - (movementSpeed - 1));
        }
        animator.SetFloat("Happiness", happiness);
        animator.SetFloat("Speed", movementSpeed);

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < distanceToPoint)
            GotoNextPoint();
    }
}