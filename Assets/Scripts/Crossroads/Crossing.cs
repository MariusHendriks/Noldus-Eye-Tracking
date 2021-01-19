using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crossing : MonoBehaviour
{
    public int pedestriansOnCrossing;
    public int carsOnCrossing;

    private List<Collider> carsWaiting = new List<Collider>();
    private List<Collider> pedestriansWaiting = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CarHitbox")
        {
            carsOnCrossing++;
        }
        else if (other.name.Contains("Clone") || other.tag == "Player")
        {
            pedestriansOnCrossing++;
        } else if (other.name == "PedestrianHitbox" && pedestriansOnCrossing > 0)
        {
            if (!carsWaiting.Contains(other))
                carsWaiting.Add(other);

            CarPathMovement carPathMovement = other.GetComponentInParent<CarPathMovement>();
            carPathMovement.waitingForCrossing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PedestrianCrossingHitbox" && carsOnCrossing > 0)
        {
            if (!pedestriansWaiting.Contains(other))
                pedestriansWaiting.Add(other);

            PathMovement pathMovement = other.GetComponentInParent<PathMovement>();
            pathMovement.waitingForCrossing = true;
        }

        if (other.name == "CarHitbox" && pedestriansOnCrossing > 0 && carsOnCrossing > 0)
        {
            if (carsWaiting.Contains(other))
                carsWaiting.Remove(other);

            CarPathMovement carPathMovement = other.GetComponentInParent<CarPathMovement>();
            carPathMovement.waitingForCrossing = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "CarHitbox")
        {
            carsOnCrossing--;

            if (carsOnCrossing <= 0)
            {
                foreach (var pedestrian in pedestriansWaiting)
                {
                    PathMovement pathMovement = pedestrian.GetComponentInParent<PathMovement>();
                    pathMovement.waitingForCrossing = false;
                }
                pedestriansWaiting.Clear();
            }
        }
        else if (other.name.Contains("Clone") || other.tag == "Player")
        {
            pedestriansOnCrossing--;

            if (pedestriansOnCrossing <= 0)
            {
                foreach (var car in carsWaiting)
                {
                    CarPathMovement carPathMovement = car.GetComponentInParent<CarPathMovement>();
                    carPathMovement.waitingForCrossing = false;
                }
                carsWaiting.Clear();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
