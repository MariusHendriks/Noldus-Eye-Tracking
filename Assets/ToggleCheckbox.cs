using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckbox : MonoBehaviour
{

    public void Toggle()
    {
        GetComponent<Toggle>().isOn = !GetComponent<Toggle>().isOn;
    }
}
