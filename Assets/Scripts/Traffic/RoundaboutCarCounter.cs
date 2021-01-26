using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundaboutCarCounter : MonoBehaviour
{
    public int carAmount;
    private List<GameObject> cars = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!cars.Contains(other.transform.parent.gameObject))
        {
            cars.Add(other.transform.parent.gameObject);
            carAmount = cars.Count;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cars.Contains(other.transform.parent.gameObject))
        {
            cars.Remove(other.transform.parent.gameObject);
            carAmount = cars.Count;
        }
    }
}
