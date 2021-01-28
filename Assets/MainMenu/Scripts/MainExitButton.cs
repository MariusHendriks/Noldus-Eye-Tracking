using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainExitButton : MainUIButton
{
    public override void Select()
    {
        Application.Quit();
    }
}
