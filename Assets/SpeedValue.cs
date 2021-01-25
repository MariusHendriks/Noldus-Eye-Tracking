using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedValue : MonoBehaviour
{
    public void ChangeAmount(float value)
    {
        GetComponent<Text>().text = "" + (value/50);
    }
}
