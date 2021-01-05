// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class PathMovement : MonoBehaviour
{
    public GameObject pointsParent;
    public Transform[] points;
    public float distanceToPoint;

    [Range(1.0f, 2.0f)]
    public float movementSpeed;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Animator animator;

    public bool debug;
    public GameObject debugPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (pointsParent != null)
        {
            Transform[] points = pointsParent.GetComponentsInChildren<Transform>();
            this.points = points;
            this.points[0] = null;
        }
        animator.SetBool("isMoving", true);


        agent.autoBraking = false;

        GotoNextPoint();

        if (debug)
        {
            Debug();
        }
    }


    private void Debug()
    {
        for (int i = 1; i < points.Length; i++)
        {
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), points[i].position, Quaternion.identity);
        }
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        if (points[destPoint] == null)
        {
            destPoint = (destPoint + 1) % points.Length;

            GotoNextPoint();
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        agent.speed = Mathf.Pow(movementSpeed, 2) + 0.5f;
        animator.SetFloat("Speed", movementSpeed);

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < distanceToPoint)
            GotoNextPoint();
    }
}