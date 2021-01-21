using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * 50f, transform.position.y, transform.position.z);
        if (transform.position.x > 1900f)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
