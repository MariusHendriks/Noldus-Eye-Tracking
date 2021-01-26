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
        GetComponent<Renderer>().material.SetFloat("_Metallic", 0.5f);
        GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.5f);
        GetComponent<Renderer>().material.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }
}
