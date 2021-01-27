/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    /*public SteamVR_LaserPointer leftLaserPointer;
    public SteamVR_LaserPointer rightLaserPointer;*/

    public Text eventText;

    void Awake()
    {
        /*leftLaserPointer.PointerIn += PointerInside;
        leftLaserPointer.PointerOut += PointerOutside;
        leftLaserPointer.PointerClick += PointerClick;
        rightLaserPointer.PointerIn += PointerInside;
        rightLaserPointer.PointerOut += PointerOutside;
        rightLaserPointer.PointerClick += PointerClick;*/
    }

    /*public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "SceneButton")
        {
            eventText.text = e.target.name + " button was clicked.";
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "SceneButton")
        {
            if(e.target.GetComponent<SceneButton>() != null)
                e.target.GetComponent<SceneButton>().moveForward = true;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "SceneButton")
        {
            if (e.target.GetComponent<SceneButton>() != null)
                e.target.GetComponent<SceneButton>().moveForward = false;
        }
    }*/
}