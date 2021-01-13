using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementAdjuster : MonoBehaviour
{
    [Range(1.5f, 25.0f)]
    public float speed;

    [Range(0, 15)]
    public float height;

    [Range(2, 9)]
    public float radius;

    private float lastRadius;
    private int numberOfObjects;
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0.0f || height == 0.0f || radius == 0.0f)
        {
            var script = GameObject.FindWithTag("VerticalMover").GetComponent<VerticalMovementSpawner>();
            speed = script.speed;
            height = script.height;
            radius = script.distance;
            numberOfObjects = script.numberOfObjects;
            lastRadius = radius;
            center = script.Center;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var x = transform.position.x;
        var z = transform.position.z;
        if (radius != lastRadius)
        {
            var objectNr = Convert.ToInt32(name);
            var spawnPos = VerticalMovementSpawner.ObjectPositionCalculator(objectNr, radius, center, numberOfObjects);
            x = spawnPos.x;
            z = spawnPos.z;
            lastRadius = radius;
        }
        //calculate what the new Y position will be
        float y = Mathf.Sin(Time.time * speed) * height + height + 1f;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(x, y, z);
    }

}
