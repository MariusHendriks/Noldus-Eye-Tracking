using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VerticalMovementUltil;

public class ChaoticVerticalSpawner : MonoBehaviour
{
    private readonly List<GameObject> objects = new List<GameObject>();
    private bool scriptIsWorking;
    private int currentNumberOfObjects;
    private float currentHeight;
    private bool customObjectsPopulated;
    private float currentSpeed;
    private float currentDistance;

    public int seed;
    public List<Mesh> customMeshes;
    public bool startScript;
    public bool customObjectsEnabler;

    [Range(1, 25)]
    public int numberOfObjects;

    public int distance;

    public float speed;

    public float height;

    public Vector3 Center { get; private set; }

    void InitializeScript()
    {
        Random.InitState(seed);
        height = Random.Range(0, 7.5f);
        Center = new Vector3(0, height, 0);
        currentNumberOfObjects = numberOfObjects;
        customObjectsPopulated = customObjectsEnabler;
        currentHeight = height;
        currentSpeed = speed;
        currentDistance = distance;
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
            else if (height != currentHeight)
            {
                ChangeObjectHeight();
            }
            else if (speed != currentSpeed)
            {
                ChangeObjectSpeed();
            }
            else if (distance != currentDistance)
            {
                ChangeDistance();
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
            else if (!startScript)
            {
                DestroyAllObjects();
                scriptIsWorking = false;
            }
        }
        else if (!scriptIsWorking && startScript)
        {
            InitializeScript();
            scriptIsWorking = true;
        }
    }

    void ChangeDistance()
    {
        if (distance < currentDistance)
        {
            foreach (var obj in objects)
            {
                var d = obj.GetComponent<VerticalMovementAdjuster>().radius;
                var percent = d * 0.1f;
                var cal = d - percent;
                if (cal > 2)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().radius = cal;
                }
            }
        }
        else
        {
            foreach (var obj in objects)
            {
                var d = obj.GetComponent<VerticalMovementAdjuster>().radius;
                var percent = d * 0.1f;
                var cal = d + percent;
                if (cal < 9)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().radius = cal;
                }
            }
        }
        currentDistance = distance;
    }

    void ChangeObjectHeight()
    {
        if (height < currentHeight)
        {
            foreach (var obj in objects)
            {
                var h = obj.GetComponent<VerticalMovementAdjuster>().height;
                var percent = h * 0.1f;
                var cal = h - percent;
                if (cal > 0)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().height = cal;
                }
            }
        }
        else
        {
            foreach (var obj in objects)
            {
                var h = obj.GetComponent<VerticalMovementAdjuster>().height;
                var percent = h * 0.1f;
                var cal = h + percent;
                if (cal < 7.5f)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().height = cal;
                }
            }
        }
        currentHeight = height;
    }

    void ChangeObjectSpeed()
    {
        if (speed < currentSpeed)
        {
            foreach (var obj in objects)
            {
                var s = obj.GetComponent<VerticalMovementAdjuster>().speed;
                var percent = s * 0.1f;
                var cal = s - percent;
                if (cal > 0.1f)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().speed = cal;
                }
            }
        }
        else
        {
            foreach (var obj in objects)
            {
                var s = obj.GetComponent<VerticalMovementAdjuster>().speed;
                var percent = s * 0.1f;
                var cal = s + percent;
                if (cal < 25f)
                {
                    obj.GetComponent<VerticalMovementAdjuster>().speed = cal;
                }
            }
        }
        currentSpeed = speed;
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
