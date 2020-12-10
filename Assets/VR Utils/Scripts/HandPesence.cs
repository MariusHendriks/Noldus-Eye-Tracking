using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPesence : MonoBehaviour
{
    public bool showController;
    public InputDeviceCharacteristics controllerCharasteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHand;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    private void TryInitialize() 
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharasteristics, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Model for " + targetDevice.name + " not found.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHand = Instantiate(handPrefab, transform);
            handAnimator = spawnedHand.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else 
        {
            if (showController)
            {
                spawnedHand.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHand.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }


    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0f);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0f);
        }
    }
}
