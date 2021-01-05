using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : WristUIButton
{
    private void OnTriggerEnter(Collider other)
    {
        Application.Quit();
    }
}
