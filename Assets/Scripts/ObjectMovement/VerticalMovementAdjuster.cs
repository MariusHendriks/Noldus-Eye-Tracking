using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementAdjuster : MonoBehaviour
{
    [Range(0.1f, 25.0f)]
    public float speed;

    [Range(0, 7.5f)]
    public float height;

    [Range(2, 9f)]
    public float radius;

    private float lastRadius;
    public int NrOfObjects { get; set; }
    public Vector3 Center { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        lastRadius = radius;
    }

    // Update is called once per frame
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
