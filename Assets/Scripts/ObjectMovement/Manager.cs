using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GD.MinMaxSlider;

public class Manager : MonoBehaviour
{
    public bool IsRunning = false;
    public int NrOfObjects;
    public int seed;

    private List<GameObject> objects = null;
    public GameObject Sablon;

    [MinMaxSlider(0,10)]
    public Vector2 Speed;

    [MinMaxSlider(0, 10)]
    public Vector2 Amplitude;


    [MinMaxSlider(-5, 5)]
    public Vector2 ElipsedX;


    [MinMaxSlider(-5, 5)]
    public Vector2 ElipsedZ;

    [MinMaxSlider(-5, 20)]
    public Vector2 DefaultAlt;

    [MinMaxSlider(0, 10)]
    public Vector2 VerticalDelta;

    [MinMaxSlider(0, 10000)]
    public Vector2 OffsetInMicros;

    [MinMaxSlider(0, 5)]
    public Vector2 Scale;

    private Movement instance;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Generate()
    {
        objects = new List<GameObject>();
        Random.InitState(seed);
        for (int i = 0; i < NrOfObjects; i++)
        {
            objects.Add(Instantiate(Sablon));
            instance = (Movement)objects[i].GetComponent(typeof(Movement));

            instance.speed = Random.Range(Speed.x, Speed.y);
            instance.amplitude = Random.Range(Amplitude.x, Amplitude.y);

            instance.elipsedX = Random.Range(ElipsedX.x, ElipsedX.y);
            while ((instance.elipsedX < 0.3 && instance.elipsedX > -0.3) || (-1.5 < instance.amplitude * instance.elipsedX && instance.amplitude * instance.elipsedX < 1.5)) 
            {
                instance.elipsedX = Random.Range(ElipsedX.x, ElipsedX.y);
            }

            instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
            while ((instance.elipsedZ < 0.3 && instance.elipsedZ > -0.3)|| (-1.5 < instance.amplitude * instance.elipsedX && instance.amplitude * instance.elipsedX < 1.5))
            {
                instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
            }

            instance.defaultAlt = Random.Range(DefaultAlt.x, DefaultAlt.y);
            instance.verticalDelta = Random.Range(VerticalDelta.x, VerticalDelta.y);
            instance.offsetInMicros = Random.Range(OffsetInMicros.x, OffsetInMicros.y);

            float scale = Random.Range(Scale.x, Scale.y);
            instance.transform.localScale = new Vector3(scale, scale, scale);


            instance.Randomizer();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (IsRunning && objects == null)
        {
            this.Generate();
            foreach (GameObject obj in objects)
            {
                instance = (Movement)obj.GetComponent(typeof(Movement));
                instance.StartMovement();
            }
        }
        if (!IsRunning && objects!= null)
        {
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            objects = null;
        }
    }
}
