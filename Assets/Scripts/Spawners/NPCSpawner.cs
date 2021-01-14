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

        PathMovement pathMovement = npc.GetComponent<PathMovement>();
        pathMovement.pointsParent = path;

        currentNPCSpawn++;

        yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));

        if (currentNPCSpawn >= maxNPCSpawn){
            StopCoroutine(SpawnNPC());

        }
        else {
            StartCoroutine(SpawnNPC());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
