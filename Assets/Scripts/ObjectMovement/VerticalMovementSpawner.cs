using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementSpawner : MonoBehaviour
{
    //public GameObject mover;
    private readonly List<GameObject> objects = new List<GameObject>();

    public int seed;
    
    [Range(0, 25)]
    public int numberOfObjects;
    
    [Range(2, 9)]
    public int distance;
    
    [Range(1.5f, 25.0f)]
    public float speed;
    
    [Range(0, 15)]
    public float height;

    public Vector3 Center { get; private set; }

    public void Start()
    {
        Random.InitState(seed);
        // Making sure the object don't go under the floor
        height /= 2;
        Center = new Vector3(0, height, 0);
        SpawnShapesAroundCenter(numberOfObjects, distance);
    }

    public void SpawnShapesAroundCenter(int num, float radius)
    {
        for (int i = 0; i < num; i++)
        {
            var spawnPos = ObjectPositionCalculator(i, radius, Center, num);
            var obj = PrimitiveTypeCreator(spawnPos, Center);
            obj.name = $"{i}";
            objects.Add(obj);
        }
    }

    GameObject PrimitiveTypeCreator(Vector3 spawnPos, Vector3 center)
    {
        PrimitiveType type;
        switch (Random.Range(0, 3))
        {
            case 0:
                type = PrimitiveType.Sphere;
                break;
            case 1:
                type = PrimitiveType.Capsule;
                break;
            case 2:
                type = PrimitiveType.Cube;
                break;
            case 3:
                type = PrimitiveType.Cylinder;
                break;
            default:
                type = PrimitiveType.Cube;
                break;
        }
        var obj = GameObject.CreatePrimitive(type);
        obj.transform.position = spawnPos;
        obj.transform.rotation = Quaternion.identity;
        // Rotate the objects to face towards center point
        obj.transform.LookAt(center);
        // Adjust height
        obj.transform.Translate(new Vector3(0, obj.transform.localScale.y / 2, 0));
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        obj.AddComponent<VerticalMovementAdjuster>();
        obj.transform.parent = transform;
        return obj;
    }

    public static Vector3 ObjectPositionCalculator(int objectNr, float radius, Vector3 center, int nrOfObjects)
    {
        var radians = 2 * Mathf.PI / nrOfObjects * objectNr;
        // Get the vector direction
        var vertrical = Mathf.Sin(radians);
        var horizontal = Mathf.Cos(radians);
        var spawnDir = new Vector3(horizontal, 0, vertrical);
        // Get the spawn position
        return center + spawnDir * radius;
    }
}
