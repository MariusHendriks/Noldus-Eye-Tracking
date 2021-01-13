using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
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
            pathObjects[i] = Instantiate(pathPrefabs[i], new Vector3(0, 0, 0), Quaternion.identity);
        }

        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

            GameObject path = pathObjects[Random.Range(0, pathObjects.Length)];
            GameObject car = Instantiate(carPrefab, this.transform.position, Quaternion.identity);

            CarPathMovement carPathMovement = car.GetComponent<CarPathMovement>();
            carPathMovement.pointsParent = path;

            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));

            StartCoroutine(SpawnCar());
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
