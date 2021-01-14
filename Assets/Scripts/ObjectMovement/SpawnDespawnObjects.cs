using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDespawnObjects : MonoBehaviour
{

    public int seed;
    public GameObject prefab;
    [Range(0, 100)]
    public int objectCount;
    [Range(0, 18)]
    public float minHeight;
    [Range(0, 18)]
    public float maxHeight;
    [Range(0, 10)]
    public float minRadius;
    [Range(0, 10)]
    public float maxRadius;
    public float objectHalfWidth;
    public float objectHalfHeight;

    public bool start;
    public float speed = 5f;
    public Mesh[] meshes;
    public List<GameObject> objects;
    
    private int meshGroup;
    private float timer;
    private int density;

    // Start is called before the first frame update
    void Start()
    {
        density = objectCount;
        objects = new List<GameObject>();
        StartCoroutine(WaitForDensityChange());
    }

    // Update is called once per frame
    void Update()
    {
        if(start && (objects == null || objects.Count == 0))
        {
            GenerateObjects(objectCount);
        }
        else if (start)
        {
            timer += Time.deltaTime;
        }
        else if (!start && objects != null && objects.Count > 0)
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
            SetObjectMesh(objects[i]);
            if(Random.Range(0, 100) > 40)
            {
                objects[i].SetActive(false);
            }
        }
        StopCoroutine(WaitToDespawn());
        StartCoroutine(WaitToDespawn());
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
        StopCoroutine(WaitToDespawn());
        StartCoroutine(WaitToDespawn());
    }

    private void SetObjectMesh(GameObject obj)
    {
        int meshNr = Random.Range(0, meshes.Length - 1);
        meshGroup = meshNr / 4;
        obj.GetComponent<MeshFilter>().mesh = meshes[meshNr];
        obj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        obj.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.85f);
        obj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.75f);
    }

    public Vector3 CalculateDirection()
    {
        var angle = Random.Range(0, 2 * Mathf.PI);
        // Get the direction of this angle
        var x = Mathf.Sin(angle);
        var z = Mathf.Cos(angle);
        return new Vector3(x, 0, z);
    }

    public IEnumerator WaitToDespawn()
    {
        while (start)
        {
                foreach (var obj in objects)
                {
                    if (Random.Range(0, 100) < 25)
                    {
                        obj.SetActive(!obj.activeSelf);
                    }
                    if (speed == 0)
                        speed = 0.1f;
                yield return new WaitForSeconds(0.5f / speed);
                }
            yield return new WaitForSeconds(1 / speed);
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
}
