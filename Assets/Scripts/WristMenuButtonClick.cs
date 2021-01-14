using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristMenuButtonClick : MonoBehaviour
{
    private Color color;

    public UnityEngine.Events.UnityEvent onButtonPressed;

    private void OnTriggerEnter(Collider other)
    {
        Color transparentColor = color;
        transparentColor.a = 0.4f;
        GetComponent<Image>().color = transparentColor;

        GetComponent<AudioSource>().Play();

        onButtonPressed.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Image>().color = color;
    }
}
