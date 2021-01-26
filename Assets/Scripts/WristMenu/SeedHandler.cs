using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedHandler : MonoBehaviour
{
    public void Decrease()
    {
        GetComponent<InputField>().text = (int.Parse(GetComponent<InputField>().text) - 1) + "";
    }

    public void Increase()
    {
        GetComponent<InputField>().text = (int.Parse(GetComponent<InputField>().text) + 1) + "";
    }
}
