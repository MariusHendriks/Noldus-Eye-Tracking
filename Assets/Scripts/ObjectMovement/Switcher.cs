using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    private bool scriptIsWorking;
    public bool orderedVerticalMovement;
    public bool chaoticVerticalMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orderedVerticalMovement && !scriptIsWorking)
        {
            var script = gameObject.GetComponent<VerticalMovementSpawner>();
            script.Play();
            scriptIsWorking = true;
        }
        else if (chaoticVerticalMovement && !scriptIsWorking)
        {
            var script = gameObject.GetComponent<ChaoticVerticalSpawner>();
            script.Play();
            scriptIsWorking = true;
        }
        else if ((!orderedVerticalMovement && !chaoticVerticalMovement) && scriptIsWorking)
        {
            scriptIsWorking = false;
        }
    }
}
