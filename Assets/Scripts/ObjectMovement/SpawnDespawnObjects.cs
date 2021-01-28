using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDespawnObjects : MonoBehaviour
{
    public bool rotation = true;
    public bool isRunning;

    public int seed;
    public GameObject prefab;
    public float speed = 1f;
    public MeshTypes meshType;
    public Mesh[] meshes;
    public List<GameObject> objects;

    [Range(0, 200)]
    public int objectCount;
    [Range(0, 18)]
    public float minHeight;
    [Range(0, 18)]
    public float maxHeight;
    [Range(2, 9)]
    public float minRadius;
    [Range(2, 9)]
    public float maxRadius;

    private float defaultRadius;
    private float defaultSpeed;

    
    private int density;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        defaultRadius = maxRadius;
        density = objectCount;
        objects = new List<GameObject>();
        StartCoroutine(WaitForDensityChange());
        StartCoroutine(WaitForRotateChange());
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning && (objects == null || objects.Count == 0))
        {
            GenerateObjects(objectCount);
        }
        else if (isRunning)
        {
        }
        else if (!isRunning && objects != null && objects.Count > 0)
        {
            DestroyObjects(objectCount);
        }
    }

    public void GenerateObjects(int amount)
    {
        Random.InitState(seed);
        for(int i = 0; i < amount; i++)
        {
            Vector3 direction = CalculateDirection();
            Vector3 position = direction * Random.Range(minRadius, maxRadius) + new Vector3(0, Random.Range(minHeight, maxHeight), 0);
            objects.Add(Instantiate(prefab, position, Quaternion.identity, transform));
            objects[i].name = "Object " + (i + 1);
            SetObjectMesh(objects[i]);

            // Scaling the shape to normal size, depending on the mesh types
            if (meshType == MeshTypes.SimpleShapes)
                objects[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            else if (meshType == MeshTypes.KitchenProps)
                objects[i].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            else if (meshType == MeshTypes.Furniture)
                objects[i].transform.localScale = new Vector3(100, 100, 100);

            objects[i].transform.localScale *= Random.Range(0.2f, 2.5f);
            if(Random.Range(0, 100) > 40)
            {
                objects[i].SetActive(false);
            }
            objects[i].transform.Rotate(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200));
            objects[i].GetComponent<ObjectRotation>().SetSpeed(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200));
            objects[i].GetComponent<ObjectRotation>().rotate = rotation;
        }
        StopCoroutine(WaitToSpawnDespawn());
        StartCoroutine(WaitToSpawnDespawn());
    }

    public void DestroyObjects(int amount)
    {
        for(int i = objects.Count - 1; amount > 0; i--)
        {
            var obj = objects[i];
            objects.Remove(obj);
            Destroy(obj);
            amount--;
        }
        StopCoroutine(WaitToSpawnDespawn());
        StartCoroutine(WaitToSpawnDespawn());
    }

    private void SetObjectMesh(GameObject obj)
    {
        int meshNr = Random.Range((int)meshType * 4 , ((int)meshType + 1) * 4);
        obj.GetComponent<MeshFilter>().mesh = meshes[meshNr];
        obj.GetComponent<MeshCollider>().sharedMesh = meshes[meshNr];
        obj.GetComponent<MeshCollider>().convex = true;
        obj.GetComponent<MeshCollider>().isTrigger = true;
        obj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        obj.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", Random.Range(0, 0.75f));
        obj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", Random.Range(0, 0.75f));
    }

    public Vector3 CalculateDirection()
    {
        var angle = Random.Range(0, 2 * Mathf.PI);
        // Get the direction of this angle
        var x = Mathf.Sin(angle);
        var z = Mathf.Cos(angle);
        return new Vector3(x, 0, z);
    }

    public IEnumerator WaitToSpawnDespawn()
    {
        while (isRunning)
        {
                foreach (var obj in objects)
                {
                    if (Random.Range(0, 100) < 25)
                    {
                        obj.SetActive(!obj.activeSelf);
                    }
                    if (speed < 0.1f)
                        speed = 0.1f;
                yield return new WaitForSeconds(0.5f / speed);
                }
            yield return new WaitForSeconds(1 / speed);
        }
    }

    public IEnumerator WaitForRotateChange()
    {
        while(true)
        {
            yield return new WaitUntil(() => !rotation);
            foreach(var obj in objects)
            {
                obj.GetComponent<ObjectRotation>().rotate = false;
            }
            yield return new WaitUntil(() => rotation);
            foreach(var obj in objects)
            {
                obj.GetComponent<ObjectRotation>().rotate = true;
            }
        }
    }

    public IEnumerator WaitForDensityChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => objectCount != density);
            if (objectCount > density)
                GenerateObjects(objectCount - density);
            else
                DestroyObjects(density - objectCount);
            density = objectCount;
        }
    }

    public void Play(int nrOfObjects, float speed, float radius, MeshTypes meshType, int seed, bool rotation)
    {
        this.objectCount = nrOfObjects;
        this.density = this.objectCount;
        this.speed = speed;
        this.maxRadius = this.defaultRadius * radius;
        this.meshType = meshType;
        this.seed = seed;
        this.rotation = rotation;
        isRunning = !isRunning;
    }

    public void ChangeNumberOfObjects(float nrOfObjects)
    {
        objectCount = (int) nrOfObjects;
    }

    public void ChangeObjectSpeed(float speed)
    {
        this.speed = this.defaultSpeed * speed;
    }

    public void ChangeObjectRadius(float radius)
    {
        maxRadius = this.defaultRadius * radius;
    }

    public void ChangeRotation(bool rotation)
    {
        this.rotation = rotation;
    }
}

public enum MeshTypes
{
    SimpleShapes,
    KitchenProps,
    Furniture
}
