using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<CarSpawn> carSpawners;
    public List<NPCSpawner> NPCSpawners;
    public Crossing crossing;

    public void EnableCars(bool enabled)
    {
        foreach(CarSpawn cs in carSpawners)
        {
            cs.enabled = enabled;
        }
    }

    public void EnableNPCs(bool enabled)
    {
        foreach(NPCSpawner ns in NPCSpawners)
        {
            ns.enabled = enabled;
        }
    }

    public void RemoveCars()
    {
        foreach(CarSpawn cs in carSpawners)
        {
            crossing.OnTriggerExit(cs.GetComponent<BoxCollider>());
            Destroy(cs);
        }
    }

    public void RemoveNPCs()
    {
        foreach (NPCSpawner ns in NPCSpawners)
        {
            crossing.OnTriggerExit(ns.GetComponent<CapsuleCollider>());
            Destroy(ns);
        }
    }
}
