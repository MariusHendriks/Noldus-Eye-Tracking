using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarsValue : MonoBehaviour
{
    public void ChangeAmount(float value)
    {
        if(value < 25)
        {
        GetComponent<Text>().text = "1";
        }
        else if (value < 50)
        {
        GetComponent<Text>().text = "2";
        }
        else if(value < 75)
        {
        GetComponent<Text>().text = "3";
        }
        else
        {
        GetComponent<Text>().text = "4";
        }
    }
}
