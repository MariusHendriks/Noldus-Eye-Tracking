using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTap : MonoBehaviour
{
    public Transform follow;

    void Start()
    {
        this.transform.parent = follow;
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
