using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public RoundaboutCarCounter RoundaboutCounter;
    public GameObject[] carPrefabs;
    public GameObject[] pathPrefabs;
    private GameObject[] pathObjects;

    public int spawnTimeMin = 1;
    public int spawnTimeMax = 20;

    // Start is called before the first frame update
    void Start()
    {
        pathObjects = new GameObject[pathPrefabs.Length];
        for (int i = 0; i < pathPrefabs.Length; i++)
        {
            pathObjects[i] = Instantiate(pathPrefabs[i], new Vector3(0, 0, 0), pathPrefabs[i].transform.rotation);
        }

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(0, 6));
        StartCoroutine(SpawnCar());
    }

    public IEnumerator SpawnCar()
    {
        while (true)
        {
            yield return new WaitUntil(() => spawnTimeMax > 0);

            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

            GameObject path = pathObjects[Random.Range(0, pathObjects.Length)];
            GameObject car = Instantiate(carPrefab, this.transform.position, Quaternion.identity);

            CarPathMovement carPathMovement = car.GetComponent<CarPathMovement>();
            carPathMovement.pointsParent = path;

            yield return new WaitUntil(() => RoundaboutCounter.carAmount < 6);
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
