using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementAdjuster : MonoBehaviour
{
    [Range(1.5f, 25.0f)]
    public float speed;

    [Range(0, 7.5f)]
    public float height;

    [Range(2, 9)]
    public float radius;

    private float lastRadius;
    private int numberOfObjects;
    private Vector3 center;
    private bool chaoticMovementEnabled;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0.0f || height == 0.0f || radius == 0.0f)
        {
            var script = GameObject.FindWithTag("VerticalMover").GetComponent<VerticalMovementSpawner>();
            height = script.height;
            radius = script.distance;
            speed = script.speed;
            numberOfObjects = script.numberOfObjects;
            chaoticMovementEnabled = script.chaoticMovementEnabled;
            lastRadius = radius;
            center = script.Center;
            chaoticMovementEnabled = script.chaoticMovementEnabled;
            if (chaoticMovementEnabled)
            { 
                speed = Random.Range(1.5f, 7f);
                height = Random.Range(0, 7.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var x = transform.position.x;
        var z = transform.position.z;
        if (radius != lastRadius)
        {
            var objectNr = System.Convert.ToInt32(name);
            var spawnDir = VerticalMovementSpawner.CalculateSpawnDirection(objectNr, numberOfObjects);
            var spawnPos = center + spawnDir * radius;
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
