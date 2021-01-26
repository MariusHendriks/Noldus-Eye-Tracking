using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public void Handle()
    {
        switch (GetComponent<Dropdown>().value)
        {
            case (0):
                GetComponent<Dropdown>().value += 1;
                break;
            case (1):
                GetComponent<Dropdown>().value += 1;
                break;
            case (2):
                GetComponent<Dropdown>().value = 0;
                break;
            default:
                break;
        }
    }
}
