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
    public MeshTypes meshType;

    private List<GameObject> objects = null;
    public GameObject Sablon;

    [MinMaxSlider(0,10)]
    public Vector2 Speed = new Vector2(0.5f, 3f);

    [MinMaxSlider(0, 10)]
    public Vector2 Amplitude = new Vector2(2f, 5f);


    [MinMaxSlider(-5, 5)]
    public Vector2 ElipsedX = new Vector2(-2f, 2f);


    [MinMaxSlider(-5, 5)]
    public Vector2 ElipsedZ = new Vector2(-2f, 2f);

    [MinMaxSlider(-5, 20)]
    public Vector2 DefaultAlt = new Vector2(2f, 6f);

    [MinMaxSlider(0, 5)]
    public Vector2 VerticalDelta = new Vector2(0f, 2f);

    [MinMaxSlider(0, 10000)]
    public Vector2 OffsetInMicros = new Vector2(0, 3000);

    [MinMaxSlider(0, 3)]
    public Vector2 Scale = new Vector2(0.2f, 1f);

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
            while ((instance.elipsedX < 0.3 && instance.elipsedX > -0.3) || (-2 < instance.amplitude * instance.elipsedX && instance.amplitude * instance.elipsedX < 2)) 
            {
                instance.elipsedX = Random.Range(ElipsedX.x, ElipsedX.y);
            }

            instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
            while ((instance.elipsedZ < 0.3 && instance.elipsedZ > -0.3)|| (-2 < instance.amplitude * instance.elipsedZ && instance.amplitude * instance.elipsedZ < 2))
            {
                instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
            }

            instance.defaultAlt = Random.Range(DefaultAlt.x, DefaultAlt.y);
            instance.verticalDelta = Random.Range(VerticalDelta.x, VerticalDelta.y);
            instance.offsetInMicros = Random.Range(OffsetInMicros.x, OffsetInMicros.y);

            float scale = Random.Range(Scale.x, Scale.y);
            if ((int) meshType == 1)
            {
                scale+=3f;
            }
            else if ((int) meshType == 2)
            {
                scale *= 200;
            }
            instance.transform.localScale = new Vector3(scale, scale, scale);


            instance.Randomizer((int) meshType);
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

    public void Play(int nrOfObjects, float minSpeed, float maxSpeed, MeshTypes meshTypes, int seed)
    {
        this.NrOfObjects = nrOfObjects;
        this.Speed = new Vector2(minSpeed, maxSpeed);
        this.meshType = meshTypes;
        this.seed = seed;
        IsRunning = !IsRunning;
    }
}
