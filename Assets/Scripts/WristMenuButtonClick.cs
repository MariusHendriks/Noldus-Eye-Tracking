using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristMenuButtonClick : MonoBehaviour
{
    private Color color;

    public UnityEngine.Events.UnityEvent onButtonPressed;

    void Start()
    {
        if(GetComponent<Image>() != null)
        color = GetComponent<Image>().color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Color transparentColor = color;
        transparentColor.a = 0.4f;

        onButtonPressed.Invoke();

        if(GetComponent<Image>() != null)
        GetComponent<Image>().color = transparentColor;

        if(GetComponent<AudioSource>() != null)
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if(GetComponent<Image>() != null)
        GetComponent<Image>().color = color;
    }
}
