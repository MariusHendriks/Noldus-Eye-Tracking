using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float time = 0;
    private bool starter = false;
    public float speed;
    public float defaultSpeed;
    public float amplitude;
    public float defaultAmplitude;
    public float elipsedX;
    public float elipsedZ;
    public float defaultAlt;
    public float verticalDelta;
    public float offsetInMicros;

    public bool highlighted = false;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForBoolChange());
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
            time = 0;
        }

    }

    public IEnumerator WaitForBoolChange()
    {
        GameObject PrefObject = this.transform.GetChild(0).gameObject;
        RotationAroundOwnAxis Cube;
        Cube = (RotationAroundOwnAxis)PrefObject.GetComponent(typeof(RotationAroundOwnAxis));

        while (true)
        {
            yield return new WaitUntil(() => highlighted);
            Cube.HighlightObject(true);

            yield return new WaitUntil(() => !highlighted);
            Cube.HighlightObject(false);
        }
    }

    public void StartMovement()
    {
        starter = true;
    }

    public bool Randomizer(int MeshSet)
    {
       
        GameObject PrefObject = this.transform.GetChild(0).gameObject;
        RotationAroundOwnAxis rotation;
        rotation = (RotationAroundOwnAxis)PrefObject.GetComponent(typeof(RotationAroundOwnAxis));
        rotation.Randomize(MeshSet);
        return true;
    }
    public void StopMovement()
    {
        starter = false;
    }

    

}
