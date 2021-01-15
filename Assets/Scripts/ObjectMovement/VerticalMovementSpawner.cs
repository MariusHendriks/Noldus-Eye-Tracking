using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VerticalMovementUltil;

public class VerticalMovementSpawner : MonoBehaviour
{
    private readonly List<GameObject> objects = new List<GameObject>();
    private bool scriptIsWorking = false;
    private float currentSpeed;
    private int currentNumberOfObjects;
    private float currentHight;
    private float currentRadius;
    private bool customObjectsPopulated;

    public int seed;
    public List<Mesh> customMeshes;
    public bool startScript;
    public bool customObjectsEnabler;    

    [Range(1, 25)]
    public int numberOfObjects;
    
    [Range(2, 9)]
    public int distance;
    
    [Range(0.1f, 25.0f)]
    public float speed;
    
    [Range(0, 7.5f)]
    public float height;

    public Vector3 Center { get; private set; }

    void InitializeScript()
    {
        Random.InitState(seed);
        Center = new Vector3(0, height, 0);
        currentSpeed = speed;
        currentNumberOfObjects = numberOfObjects;
        currentHight = height;
        currentRadius = distance;
        customObjectsPopulated = customObjectsEnabler;
        SpawnShapesAroundCenter(numberOfObjects, distance);
    }

    public void Update()
    {
        if (speed != currentSpeed && scriptIsWorking)
        {
            foreach (var obj in objects)
            {
                obj.GetComponent<VerticalMovementAdjuster>().speed = speed;
            }
            currentSpeed = speed;
        }
        if (height != currentHight && scriptIsWorking)
        {
            foreach (var obj in objects)
            {
                obj.GetComponent<VerticalMovementAdjuster>().height = height;
            }
            currentHight = height;
        }
        if (distance != currentRadius && scriptIsWorking)
        {
            foreach (var obj in objects)
            {
                obj.GetComponent<VerticalMovementAdjuster>().radius = distance;
            }
            currentRadius = distance;
        }
        if (numberOfObjects != currentNumberOfObjects && scriptIsWorking)
        {
            DestroyAllObjects();
            InitializeScript();
            currentNumberOfObjects = numberOfObjects;
        }
        if (startScript && !scriptIsWorking)
        {
            InitializeScript();
            scriptIsWorking = true;
        }
        else if (!startScript && scriptIsWorking)
        {
            DestroyAllObjects();
            scriptIsWorking = false;
        }
        if (customObjectsEnabler && !customObjectsPopulated && startScript)
        {
            DestroyAllObjects();
            InitializeScript();
            customObjectsPopulated = true;
        }
        else if (!customObjectsEnabler && customObjectsPopulated)
        {
            DestroyAllObjects();
            InitializeScript();
            customObjectsPopulated = false;
        }
    }

    public void SpawnShapesAroundCenter(int num, float radius)
    {
        for (int i = 0; i < num; i++)
        {
            var spawnDir = CalculateSpawnDirection(i, num);
            var spawnPos = Center + spawnDir * radius;
            GameObject obj;
            if (customObjectsEnabler)
                obj = CustomMeshSpawner(spawnPos, Center, transform, customMeshes);
            else 
                obj = PrimitiveTypeCreator(spawnPos, Center, transform);
            var script = obj.GetComponent<VerticalMovementAdjuster>();
            script.speed = speed;
            script.height = height;
            script.radius = radius;
            script.NrOfObjects = num;
            script.Center = Center;
            obj.name = $"{i}";
            objects.Add(obj);
        }
    }

    void DestroyAllObjects()
    {
        objects.ForEach((obj) => Destroy(obj));
        objects.Clear();
    }
}
