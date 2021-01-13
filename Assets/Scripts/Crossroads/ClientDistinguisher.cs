using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDistinguisher : MonoBehaviour
{
    public enum Client { Fontys, Noldus}
    public Client client = Client.Fontys;


    // Start is called before the first frame update
    void Start()
    {
        if (client == Client.Noldus)
        {
            this.transform.position = new Vector3(0, 0.336f, 0);
        } else if (client == Client.Fontys)
        {
            this.transform.position = new Vector3(-14.20257f, 0, -15.81555f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
