using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MovementStartHandler : MonoBehaviour
{
    private int nrOfObjects;

    private float speed;

    private float distance;

    private MeshTypes meshType;
    
    private int seed;

    private bool isChaotic;
    private bool rotation;

    public MovementType movementType;

    public Transform tornadoSettings;
    public Transform verticalSettings;
    public Transform appearingDisappearingSettings;

    public OutputToJSON Logger;

    public void GetValues()
    {
        switch (movementType)
        {
            case MovementType.Tornado:
                GetMovementSettings(tornadoSettings);
                break;
            case MovementType.Vertical:
                GetMovementSettings(verticalSettings);
                isChaotic = verticalSettings.GetChild(0).GetChild(5).GetComponent<Toggle>().isOn;
                break;
            case MovementType.AppearingDisappearing:
                GetMovementSettings(appearingDisappearingSettings);
                rotation = appearingDisappearingSettings.GetChild(0).GetChild(5).GetComponent<Toggle>().isOn;
                break;
            default:
                break;
        }
    }

    private void GetMovementSettings(Transform movementType)
    {
        Transform settings = movementType.GetChild(0);
        nrOfObjects = int.Parse(settings.GetChild(0).GetChild(2).GetComponent<Text>().text);
        speed = float.Parse(settings.GetChild(1).GetChild(2).GetComponent<Text>().text);
        distance = float.Parse(settings.GetChild(2).GetChild(2).GetComponent<Text>().text);
        meshType = (MeshTypes) settings.GetChild(3).GetChild(1).GetComponent<Dropdown>().value;
        seed = int.Parse(settings.GetChild(4).GetChild(1).GetComponent<InputField>().text);

    }

    public void StartWithSettings()
    {
        if(GetComponentInChildren<Text>().text == "Start")
        {
            Logger.simulationWorking = true;
            GetComponentInChildren<Text>().text = "Stop";
        }
        else
        {
            Logger.simulationWorking = false;
            GetComponentInChildren<Text>().text = "Start";
        }
        GetValues();
        switch (movementType)
        {
            case MovementType.Tornado:
                FindObjectOfType<Manager>().Play(nrOfObjects, speed, distance, meshType, seed);
                break;
            case MovementType.Vertical:
                FindObjectOfType<Switcher>().Play(nrOfObjects, speed, distance, meshType, seed, isChaotic);
                break;
            case MovementType.AppearingDisappearing:
                FindObjectOfType<SpawnDespawnObjects>().Play(nrOfObjects, speed, distance, meshType, seed, rotation);
                break;
            default:
                break;
        }
    }
}

public enum MovementType
{
    Tornado,
    Vertical,
    AppearingDisappearing
}