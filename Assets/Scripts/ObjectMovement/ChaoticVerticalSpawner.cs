using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VerticalMovementUltil;

public class ChaoticVerticalSpawner : MonoBehaviour
{
    private readonly List<GameObject> objects = new List<GameObject>();
    private bool scriptIsWorking;
    private int currentNumberOfObjects;
    private float height;
    private bool customObjectsPopulated;

    public int seed;
    public List<Mesh> customMeshes;
    public bool startScript;
    public bool customObjectsEnabler;

    [Range(1, 25)]
    public int numberOfObjects;

    public Vector3 Center { get; private set; }

    void InitializeScript()
    {
        Random.InitState(seed);
        height = Random.Range(0, 7.5f);
        Center = new Vector3(0, height, 0);
        currentNumberOfObjects = numberOfObjects;
        customObjectsPopulated = customObjectsEnabler;
        SpawnShapesAroundCenter(numberOfObjects);
    }

    public void Update()
    {
        if (numberOfObjects != currentNumberOfObjects && scriptIsWorking)
        {
            ResetScene();
            currentNumberOfObjects = numberOfObjects;
        }
        if (customObjectsEnabler && !customObjectsPopulated && startScript)
        {
            ResetScene();
            customObjectsPopulated = true;
        }
        else if (!customObjectsEnabler && customObjectsPopulated && startScript)
        {
            ResetScene();
            customObjectsPopulated = false;
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
    }

    public void SpawnShapesAroundCenter(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var radius = Random.Range(2, 9f);
            var spawnDir = CalculateSpawnDirection(i, num);
            var spawnPos = Center + spawnDir * radius;
            GameObject obj;
            if (customObjectsEnabler)
                obj = CustomMeshSpawner(spawnPos, Center, transform, customMeshes);
            else
                obj = PrimitiveTypeCreator(spawnPos, Center, transform);
            obj = SetVerticalAdjusterScriptParameters(obj, radius, num, Center, Random.Range(1.5f, 7f), Random.Range(0, 7.5f));
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
