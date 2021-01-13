using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementSpawner : MonoBehaviour
{
    public GameObject mover;
    
    public int seed;
    [Range(0, 25)]
    public int numberOfObjects;
    [Range(0, 9)]
    public int distance;
    [Range(0.0f, 25.0f)]
    public float speed;
    [Range(0, 15)]
    public float height;

    public void Start()
    {
        // Making sure the object don't go under the floor
        height /= 2;
        // Centering the parent pived with the object
        mover.transform.position = new Vector3(mover.transform.position.x, height, mover.transform.position.z);
        Random.InitState(seed);
        SpawnShapesAroundCenter(numberOfObjects, distance);
    }

    void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + height + 0.55f;
        //set the object's Y to the new calculated Y
        mover.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void SpawnShapesAroundCenter(int num, float radius)
    {
        var center = new Vector3(0, height, 0);
        for (int i = 0; i < num; i++)
        {
            // Distance around the circle
            var radians = 2 * Mathf.PI / num * i;
            // Get the vector direction
            var vertrical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);
            var spawnDir = new Vector3(horizontal, 0, vertrical);
            // Get the spawn position
            var spawnPos = center + spawnDir * radius; // Radius is just the distance away from the point
            PrimitiveTypeCreator(spawnPos, center);
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
        obj.transform.parent = mover.transform;
        return obj;
    }

}
