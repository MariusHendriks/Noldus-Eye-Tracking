using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAmountValue : MonoBehaviour
{
    public void ChangeAmount(float value)
    {
        GetComponent<Text>().text = "" + (value*2);
    }
}
