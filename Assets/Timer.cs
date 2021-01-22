using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float secondsStillStart;
    public bool IsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        secondsStillStart = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            secondsStillStart += Time.deltaTime;
        }
        else
        {
            secondsStillStart = 0;
        }
    }

    public void StartTimer()
    {
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public float GetTimerTime()
    {
        return secondsStillStart;
    }
}
