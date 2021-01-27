using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<CarSpawn> carSpawners;
    public List<NPCSpawner> NPCSpawners;
    public List<PathMovement> NPCs;
    public List<CarPathMovement> Cars;

    public void EnableCars(bool enabled)
    {
        foreach(CarSpawn cs in carSpawners)
        {
            cs.gameObject.SetActive(enabled);
        }
        if(!enabled)
        {
            RemoveCars();
        }
    }

    public void EnableNPCs(bool enabled)
    {
        foreach(NPCSpawner ns in NPCSpawners)
        {
            ns.gameObject.SetActive(enabled);
        }
        if(!enabled)
        {
            RemoveNPCs();
        }
    }

    public void RemoveCars()
    {
        Cars.Clear();
        Cars.AddRange(FindObjectsOfType<CarPathMovement>());
        foreach(CarPathMovement car in Cars)
        {
            if (car.crossing != null)
            {
                BoxCollider collider = car.GetComponentsInChildren<BoxCollider>().First((box) => box.name == "CarHitbox");
                car.crossing.OnTriggerExit(collider);
            }
            Destroy(car.gameObject);
        }
    }

    public void RemoveNPCs()
    {
        NPCs.Clear();
        NPCs.AddRange(FindObjectsOfType<PathMovement>());
        foreach (PathMovement npc in NPCs)
        {
            if (npc.crossing != null)
            {
                npc.crossing.OnTriggerExit(npc.GetComponent<CapsuleCollider>());
            }
            Destroy(npc.gameObject);
        }
    }

    public void AdjustMaxNPCS(int maxNPCs)
    {
        foreach(NPCSpawner ns in NPCSpawners)
        {
            ns.maxNPCSpawn = maxNPCs / NPCSpawners.Count();
        }
    }
}
