using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VerticalMovementUltil;

public class ChaoticVerticalSpawner : MonoBehaviour
{
    private readonly List<GameObject> objects = new List<GameObject>();
    private bool scriptIsWorking;

    private int currentNumberOfObjects;
    private float currentSpeed;
    private float currentRadius;
    private MeshTypes currentType;

    public MeshTypes meshType;
    public int seed;
    public List<Mesh> meshes;
    public bool IsRunning;
    public bool customObjectsEnabler;

    [Range(1, 25)]
    public int numberOfObjects;

    public float radius = 1;

    public float speed = 1;

    public float height;

    public Vector3 Center { get; private set; }

    void InitializeScript()
    {
        Random.InitState(seed);
        height = Random.Range(0, 7.5f);
        Center = new Vector3(0, height, 0);
        currentNumberOfObjects = numberOfObjects;
        currentSpeed = speed;
        currentRadius = radius;
        currentType = meshType;
        SpawnShapesAroundCenter(numberOfObjects);
    }

    public void Update()
    {
        if (scriptIsWorking)
        {
            if (numberOfObjects != currentNumberOfObjects)
            {
                ResetScene();
                currentNumberOfObjects = numberOfObjects;
            }
            else if (speed != currentSpeed)
            {
                ChangeObjectSpeed(speed);
            }
            else if (radius != currentRadius)
            {
                ChangeObjectRadius(radius);
            }
            else if (meshType != currentType)
            {
                ResetScene();
                currentType = meshType;
            }
            else if (!IsRunning)
            {
                DestroyAllObjects();
                scriptIsWorking = false;
            }
        }
        else if (!scriptIsWorking && IsRunning)
        {
            InitializeScript();
            scriptIsWorking = true;
        }
    }

    public void ChangeNumberOfObjects(int nrOfObjects)
    {
        numberOfObjects = nrOfObjects;
    }

    public void ChangeObjectRadius(float radius)
    {
        foreach (var obj in objects)
        {
            var script = obj.GetComponent<VerticalMovementAdjuster>();
            script.radius = script.DefaultRadius * radius;
        }
        this.radius = currentRadius = radius;
    }

    public void ChangeObjectSpeed(float speed)
    {
        foreach (var obj in objects)
        {
            var script = obj.GetComponent<VerticalMovementAdjuster>();
            script.speed = script.DefaultSpeed * speed;
        }
        this.speed = currentSpeed = speed;
    }

    public void SpawnShapesAroundCenter(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var radius = Random.Range(2, 9f) * this.radius;
            var spawnDir = CalculateSpawnDirection(i, num);
            var spawnPos = Center + spawnDir * radius;
            GameObject obj = CreateMeshes(spawnPos, Center, transform, meshType, meshes);
            obj = SetVerticalAdjusterScriptParameters(obj, radius, num, Center, Random.Range(1.5f, 7f) * speed, Random.Range(0, 7.5f));
            obj.name = $"{i + 1}";
            objects.Add(obj);
        }
    }

    void ResetScene()
    {
        DestroyAllObjects();
        InitializeScript();
    }

    void DestroyAllObjects()
    {
        objects.ForEach((obj) => Destroy(obj));
        objects.Clear();
    }

    public void Play(int nrOfObjects, float speed, float distance, MeshTypes meshType, int seed)
    {
        this.numberOfObjects = nrOfObjects;
        this.speed = speed;
        this.radius = distance;
        this.seed = seed;
        this.meshType = meshType;
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }
}
