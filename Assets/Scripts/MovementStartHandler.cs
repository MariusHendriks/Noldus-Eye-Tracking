using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MovementStartHandler : MonoBehaviour
{
    private int nrOfObjects;

    private float minSpeed;
    private float speed;

    private float maxSpeed;
    private float distance;
    private float maximumRadius;

    private MeshTypes meshType;
    
    private int seed;

    private bool isChaotic;

    public MovementType movementType;

    public Transform tornadoSettings;
    public Transform verticalSettings;
    public Transform appearingDisappearingSettings;

    public void GetValues()
    {
        switch (movementType)
        {
            case MovementType.Tornado:
                GetMovementSettings(tornadoSettings);
                minSpeed = int.Parse(tornadoSettings.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().text);
                maxSpeed = int.Parse(tornadoSettings.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text);
                break;
            case MovementType.Vertical:
                GetMovementSettings(verticalSettings);
                speed = int.Parse(verticalSettings.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().text);
                distance = int.Parse(verticalSettings.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text);
                isChaotic = verticalSettings.GetChild(0).GetChild(5).GetComponent<Toggle>().IsActive();
                break;
            case MovementType.AppearingDisappearing:
                GetMovementSettings(appearingDisappearingSettings);
                speed = int.Parse(appearingDisappearingSettings.GetChild(0).GetChild(1).GetChild(2).GetComponent<Text>().text);
                maximumRadius = int.Parse(appearingDisappearingSettings.GetChild(0).GetChild(2).GetChild(2).GetComponent<Text>().text);
                break;
            default:
                break;
        }
    }

    private void GetMovementSettings(Transform movementType)
    {
        Transform settings = movementType.GetChild(0);
        nrOfObjects = int.Parse(settings.GetChild(0).GetChild(2).GetComponent<Text>().text);
        meshType = (MeshTypes) settings.GetChild(3).GetChild(1).GetComponent<Dropdown>().value;
        seed = int.Parse(settings.GetChild(4).GetChild(1).GetComponent<InputField>().text);

    }

    public void StartWithSettings()
    {
        GetValues();
        switch (movementType)
        {
            case MovementType.Tornado:
                FindObjectOfType<Manager>().Play(nrOfObjects, minSpeed, maxSpeed, meshType, seed);
                break;
            case MovementType.Vertical:
                FindObjectOfType<Manager>().Play(nrOfObjects, minSpeed, maxSpeed, meshType, seed);
                break;
            case MovementType.AppearingDisappearing:
                FindObjectOfType<SpawnDespawnObjects>().Play(nrOfObjects, speed, maximumRadius, meshType, seed);
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