using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float time = 0;
    private bool starter = false;
    public float speed;
    public float amplitude;
    public float elipsedX;
    public float elipsedZ;
    public float defaultAlt;
    public float verticalDelta;
    public float offsetInMicros;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (starter)
        {
            time += Time.deltaTime;
            Vector3 nextPos = new Vector3((amplitude * elipsedX) * Mathf.Cos(speed * time), defaultAlt + verticalDelta * Mathf.Cos(speed * (time + (offsetInMicros / 1000))), (amplitude * elipsedZ) * Mathf.Sin(speed * time));
            this.transform.position = Vector3.MoveTowards(transform.position, nextPos, 999);

        }
        else
        {
            time = 0; ;
        }
    }

    public void StartMovement()
    {
        starter = true;
    }

    public bool Randomizer()
    {
        speed = Random.Range(1f, 4f);
        amplitude = Random.Range(2f, 5f);
        elipsedX = Random.Range(-0.5f, 1.5f);
        if (elipsedX<0.5f)
        {
            elipsedX -= 1;
        }
        elipsedZ = Random.Range(-0.5f, 1.5f);
        if (elipsedZ < 0.5f)
        {
            elipsedZ -= 1;
        }
        defaultAlt = Random.Range(-1f, 4f);
        verticalDelta = Random.Range(0f, 2f);
        offsetInMicros = Random.Range(0, 3000);
        float scale = Random.Range(0.1f, 1f);
        this.transform.localScale = new Vector3(scale, scale, scale);
        GameObject PrefObject = this.transform.GetChild(0).gameObject;
        RotationAroundOwnAxis rotation;
        rotation = (RotationAroundOwnAxis)PrefObject.GetComponent(typeof(RotationAroundOwnAxis));
        rotation.Randomize();
        return true;
    }
}
