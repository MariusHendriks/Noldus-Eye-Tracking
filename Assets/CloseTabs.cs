using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTabs : MonoBehaviour
{
    public GameObject[] tabsToClose;

    public void HideTabs()
    {
        foreach (GameObject tab in tabsToClose)
        {
            tab.SetActive(false);
        }
    }
}
