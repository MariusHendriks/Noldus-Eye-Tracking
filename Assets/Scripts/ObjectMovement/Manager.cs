using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int NrOfObjects;
    public int seed;
    private List<GameObject> objects;
    public GameObject Sablon;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        Random.InitState(seed);
        for (int i = 0; i < NrOfObjects; i++)
        {
            objects.Add(Instantiate(Sablon));
            Movement instance = (Movement)objects[i].GetComponent(typeof(Movement));
            
            instance.Randomizer();
            instance.StartMovement();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
