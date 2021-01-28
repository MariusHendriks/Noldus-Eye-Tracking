using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementAdjuster : MonoBehaviour
{
    private float lastRadius;
    public float DefaultSpeed { get; set; }
    public float DefaultRadius { get; set; }
    public float DefaultHeight { get; set; }

    [Range(0.1f, 25f)]
    public float speed;

    [Range(0, 7.5f)]
    public float height;

    [Range(2, 9f)]
    public float radius;

    public int NrOfObjects { get; set; }
    public Vector3 Center { get; set; }

    void Start()
    {
        DefaultRadius = lastRadius = radius;
        DefaultSpeed = speed;
    }

    void Update()
    {
        var x = transform.position.x;
        var z = transform.position.z;
        if (radius != lastRadius)
        {
            var objectNr = System.Convert.ToInt32(name);
            var spawnDir = VerticalMovementUltil.CalculateSpawnDirection(objectNr, NrOfObjects);
            var spawnPos = Center + spawnDir * radius;
            x = spawnPos.x;
            z = spawnPos.z;
            lastRadius = radius;
        }
        var y = Mathf.Sin(Time.time * speed) * height + height + 1f;
        transform.position = new Vector3(x, y, z);
    }
}
