using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    public bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
