using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristUIButton : MonoBehaviour
{
    public GameObject[] sidePanels;
    public int sidePanelNumber;
    private Color color;

    void Start()
    {
        color = GetComponent<Image>().color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Color transparentColor = color;
        transparentColor.a = 0.4f;
        GetComponent<Image>().color = transparentColor;

        GetComponent<AudioSource>().Play();

        if(sidePanels.Length > 0)
        {
            foreach (var sidePanel in sidePanels)
            {
                sidePanel.SetActive(false);
            }
            sidePanels[sidePanelNumber - 1].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Image>().color = color;
    }
}
