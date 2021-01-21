﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WristUISlider : WristUIElement
{
    public GameObject handle;
    public GameObject rightHand;
    public Transform startPoint;

    private Slider slider;

    private Transform indexTransform;

    private AudioSource audioSource;

    [Serializable]
    public class SliderEvent : UnityEvent<float> { }

    [SerializeField]
    private SliderEvent onSliderReleased = new SliderEvent();

    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();

        FingerTap[] children = rightHand.GetComponentsInChildren<FingerTap>();

        indexTransform = children[0].transform;
        //StartCoroutine(waitRightHand());

        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator waitRightHand()
    {
        yield return new WaitForSeconds(3);
        FingerTap[] children = rightHand.GetComponentsInChildren<FingerTap>();

        indexTransform = children[0].transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if(indexTransform != null)
        {
            float d = startPoint.InverseTransformPoint(indexTransform.position).x - startPoint.InverseTransformPoint(startPoint.position).x;
            float value = d * 0.66666f;
            slider.value = value > 0 ? value : 0;
        }
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        yield return new WaitForFixedUpdate();
        onSliderReleased.Invoke(float.Parse(transform.parent.GetChild(2).GetComponent<Text>().text));
        Debug.Log("Send current value of slider");
    }

    private float lastSound;

    public void OnValueChange(float value)
    {
        if (Time.time > lastSound + 0.05f)
        {
            audioSource.pitch = value / 200 + 0.7f;
            audioSource.Play();
            lastSound = Time.time;
        }
    }
}
