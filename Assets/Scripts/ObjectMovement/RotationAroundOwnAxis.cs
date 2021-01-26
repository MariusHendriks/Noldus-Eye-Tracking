using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAroundOwnAxis : MonoBehaviour
{
    public float SpeedX;

    public float SpeedY;

    public float SpeedZ;

    public Mesh[] meshes = new Mesh[4];


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.right, SpeedX * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.up, SpeedY * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.forward, SpeedZ * Time.deltaTime);
    }

    public void Randomize(int MeshSet)
    {
        SpeedX = Random.Range(-200, 200);
        SpeedY = Random.Range(-200, 200);
        SpeedZ = Random.Range(-200, 200);
        MeshFilter mesh = this.gameObject.GetComponent<MeshFilter>();
        mesh.sharedMesh = meshes[Random.Range((MeshSet*4), (MeshSet+1)*4)];
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh.sharedMesh;
        this.gameObject.GetComponent<MeshCollider>().convex = true;
        this.gameObject.GetComponent<MeshCollider>().isTrigger = true;

        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.5f);
        GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.5f);
        GetComponent<Renderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }

    public void HighlightObject(bool highlight)
    {
        if (highlight)
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
