using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VerticalMovementUltil
{
    public static GameObject CustomMeshSpawner(Vector3 spawnPos, Vector3 center, Transform transform, List<Mesh> customMeshes)
    {
        var i = Random.Range(0, 5);
        var obj = new GameObject();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<MeshCollider>();
        obj.GetComponent<MeshCollider>().sharedMesh = customMeshes[i];
        obj.GetComponent<MeshFilter>().mesh = customMeshes[i];
        obj = DefaultGameObjectSettings(obj, spawnPos, center, transform);
        var scale = Random.Range(0f, 3f);
        scale *= 1.5f;
        obj.transform.localScale = new Vector3(scale, scale, scale);
        return obj;
    }

    public static GameObject PrimitiveTypeCreator(Vector3 spawnPos, Vector3 center, Transform transform)
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
            default:
                type = PrimitiveType.Cylinder;
                break;
        }
        var obj = GameObject.CreatePrimitive(type);
        obj = DefaultGameObjectSettings(obj, spawnPos, center, transform);
        return obj;
    }

    public static GameObject DefaultGameObjectSettings(GameObject obj, Vector3 spawnPos, Vector3 center, Transform transform)
    {
        obj.transform.position = spawnPos;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.LookAt(center);
        obj.transform.Translate(new Vector3(0, obj.transform.localScale.y / 2, 0));
        obj.transform.parent = transform;
        var material = obj.GetComponent<Renderer>().material;
        material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        material.SetFloat("_Metallic", 0.97f);
        material.SetFloat("_Glossiness", 1);
        obj.AddComponent<VerticalMovementAdjuster>();
        return obj;
    }

    public static GameObject SetVerticalAdjusterScriptParameters(GameObject obj, float radius, int num, Vector3 center, float speed, float height)
    {
        var script = obj.GetComponent<VerticalMovementAdjuster>();
        script.radius = radius;
        script.speed = speed;
        script.height = height;
        script.Center = center;
        script.NrOfObjects = num;
        return obj;
    }

    public static Vector3 CalculateSpawnDirection(int objectNr, int nrOfObjects)
    {
        var radians = 2 * Mathf.PI / nrOfObjects * objectNr;
        // Get the vector direction
        var vertrical = Mathf.Sin(radians);
        var horizontal = Mathf.Cos(radians);
        return new Vector3(horizontal, 0, vertrical);
    }
}
