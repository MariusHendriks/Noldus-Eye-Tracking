using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float SpeedX;

    public float SpeedY;

    public float SpeedZ;

    public bool rotate;
    private Quaternion defaultRotation;

    private void Start()
    {
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.RotateAround(transform.position, Vector3.right, SpeedX * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.up, SpeedY * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.forward, SpeedZ * Time.deltaTime);
        }
        else
        {
            transform.rotation = defaultRotation;
        }
    }

    public void SetSpeed(float speedX, float speedY, float speedZ)
    {
        SpeedX = speedX;
        SpeedY = speedY;
        SpeedZ = speedZ;
    }
}
