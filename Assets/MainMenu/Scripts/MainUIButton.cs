using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainUIButton : MonoBehaviour
{
    [HideInInspector] public bool moveForward = false;
    public float moveSpeed = 0.5f;

    void FixedUpdate()
    {
        if (moveForward)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, -30), moveSpeed);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, -9), moveSpeed);
        }
    }

    public void HoverEnter()
    {
        moveForward = true;
    }

    public void HoverExit()
    {
        moveForward = false;
    }

    // Update is called once per frame
    public abstract void Select();
}
