using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHover : MonoBehaviour
{
    private bool colliding = false;
    private List<string> collisions = new List<string>();

    public GameObject fingerTap;

    private void Start()
    {
        collisions = new List<string>();
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        yield return new WaitForFixedUpdate();
        colliding = true;

    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        yield return new WaitForFixedUpdate();
        Debug.Log("Send current value of slider");
    }

    private void FixedUpdate()
    {
        fingerTap.GetComponent<MeshRenderer>().enabled = colliding;

        colliding = false;
    }
}
