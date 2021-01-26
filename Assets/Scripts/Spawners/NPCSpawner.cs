using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] NPCPrefabs;
    public GameObject[] pathPrefabs;
    private GameObject[] pathObjects;


    public int maxNPCSpawn = 30;
    public int spawnTimeMin = 1;
    public int spawnTimeMax = 20;

    public float speed = 3f;

    private int currentNPCSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathObjects = new GameObject[pathPrefabs.Length];
        for (int i = 0; i < pathPrefabs.Length; i++)
        {
            pathObjects[i] = Instantiate(pathPrefabs[i], new Vector3(0, 0, 0), pathPrefabs[i].transform.rotation);
        }

        StartCoroutine(SpawnNPC());
    }

    private IEnumerator SpawnNPC()
    {


        GameObject npcPrefab = NPCPrefabs[Random.Range(0, NPCPrefabs.Length)];

        GameObject path = pathObjects[Random.Range(0, pathObjects.Length)];
        GameObject npc = Instantiate(npcPrefab, this.transform.position, Quaternion.identity);

        npc.layer = 19;

        PathMovement pathMovement = npc.GetComponent<PathMovement>();
        pathMovement.pointsParent = path;
        pathMovement.movementSpeed = RandomGaussian(0f, 2f);
        if (pathMovement.movementSpeed <= 1)
        {
            pathMovement.movementSpeed = Random.Range(1f, 1.1f);
        }
        pathMovement.happiness = RandomGaussian(-1, 1);

        currentNPCSpawn++;

        yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));

        if (currentNPCSpawn >= maxNPCSpawn)
        {
            StopCoroutine(SpawnNPC());

        }
        else
        {
            StartCoroutine(SpawnNPC());
        }

    }

    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
