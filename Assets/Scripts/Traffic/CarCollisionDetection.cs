using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetection : MonoBehaviour
{
    public bool isColliding = false;

    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
