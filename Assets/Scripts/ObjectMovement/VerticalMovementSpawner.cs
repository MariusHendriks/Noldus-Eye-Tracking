using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VerticalMovementUltil;

public class VerticalMovementSpawner : MonoBehaviour
{
    private readonly List<GameObject> objects = new List<GameObject>();
    private bool scriptIsWorking;
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
    
    [Range(0.1f, 25f)]
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
        if (scriptIsWorking)
        {
            if (speed != currentSpeed)
            {
                foreach (var obj in objects)
                    obj.GetComponent<VerticalMovementAdjuster>().speed = speed;
                currentSpeed = speed;
            }
            else if (height != currentHight)
            {
                foreach (var obj in objects)
                    obj.GetComponent<VerticalMovementAdjuster>().height = height;
                currentHight = height;
            }
            else if (distance != currentRadius)
            {
                foreach (var obj in objects)
                    obj.GetComponent<VerticalMovementAdjuster>().radius = distance;
                currentRadius = distance;
            }
            else if (numberOfObjects != currentNumberOfObjects)
            {
                ResetScene();
                currentNumberOfObjects = numberOfObjects;
            }
            else if (!startScript)
            {
                DestroyAllObjects();
                scriptIsWorking = false;
            }
            else if (customObjectsEnabler && !customObjectsPopulated)
            {
                ResetScene();
                customObjectsPopulated = true;
            }
            else if (!customObjectsEnabler && customObjectsPopulated)
            {
                ResetScene();
                customObjectsPopulated = false;
            }
        }
        else if (!scriptIsWorking && startScript)
        {
            InitializeScript();
            scriptIsWorking = true;
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
            SetVerticalAdjusterScriptParameters(obj, radius, num, Center, speed, height);
            obj.name = $"{i}";
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

    public void Play()
    {
        startScript = !startScript;
    }
}
