using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    private bool scriptIsWorking = false;
    public bool isChaotic = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play(int nrOfObjects, float speed, float distance, MeshTypes meshType, int seed, bool isChaotic)
    {
        this.isChaotic = isChaotic;
        if (!scriptIsWorking)
        {
            if (isChaotic)
            {
                gameObject.GetComponent<VerticalMovementSpawner>().Stop();
                gameObject.GetComponent<ChaoticVerticalSpawner>().Play(nrOfObjects, speed, distance, meshType, seed);
            }
            else
            {
                gameObject.GetComponent<ChaoticVerticalSpawner>().Stop();
                gameObject.GetComponent<VerticalMovementSpawner>().Play(nrOfObjects, speed, distance, meshType, seed);
            }
        }
        else
        {
            if(isChaotic)
            {
                gameObject.GetComponent<ChaoticVerticalSpawner>().Stop();
                gameObject.GetComponent<VerticalMovementSpawner>().Stop();
            }
            else
            {
                gameObject.GetComponent<ChaoticVerticalSpawner>().Stop();
                gameObject.GetComponent<VerticalMovementSpawner>().Stop();
            }
        }
        scriptIsWorking = !scriptIsWorking;
    }

    public void ChangeNumberOfObjects(float numberOfObjects)
    {
        if(isChaotic)
        {
            gameObject.GetComponent<ChaoticVerticalSpawner>().ChangeNumberOfObjects((int)numberOfObjects);
        }
        else
        {
            gameObject.GetComponent<VerticalMovementSpawner>().ChangeNumberOfObjects((int)numberOfObjects);
        }
    }

    public void ChangeSpeed(float speed)
    {
        if(isChaotic)
        {
            gameObject.GetComponent<ChaoticVerticalSpawner>().ChangeObjectSpeed(speed);
        }
        else
        {
            gameObject.GetComponent<VerticalMovementSpawner>().ChangeObjectSpeed(speed);
        }
    }

    public void ChangeRadius(float radius)
    {
        if (isChaotic)
        {
            gameObject.GetComponent<ChaoticVerticalSpawner>().ChangeObjectRadius(radius);
        }
        else
        {
            gameObject.GetComponent<VerticalMovementSpawner>().ChangeObjectRadius(radius);
        }
    }
}
