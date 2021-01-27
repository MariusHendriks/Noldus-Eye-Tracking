using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTab : MonoBehaviour
{
    public GameObject tabToClose;

    public void HideTab()
    {
        tabToClose.SetActive(false);
    }
}
