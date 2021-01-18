using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementAdjuster : MonoBehaviour
{
    private bool currentPriority;
    private float lastRadius;
    private Material defaultMat;

    [Range(0.1f, 25f)]
    public float speed;

    [Range(0, 7.5f)]
    public float height;

    [Range(2, 9f)]
    public float radius;

    public bool makePriority;

    public int NrOfObjects { get; set; }
    public Vector3 Center { get; set; }

    void Start()
    {
        lastRadius = radius;
        defaultMat = gameObject.GetComponent<MeshRenderer>().material;
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
        if (makePriority && !currentPriority)
        {
            var loadedMat = Resources.Load("Materials/RandomObjectMaterial") as Material;
            ChangeMaterial(loadedMat);
            currentPriority = true;
        }
        else if (!makePriority && currentPriority)
        {
            ChangeMaterial(defaultMat);
            currentPriority = makePriority;
        }
    }

    void ChangeMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
