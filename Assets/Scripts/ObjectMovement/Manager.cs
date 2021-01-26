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

    private float previousSpeedMultiplier = 1;
    public float SpeedMultiplier = 1;

    private float previousAmplitudeMultiplier = 1;
    public float AmplitudeMultiplier = 1;

    public MeshTypes meshType;

    private List<GameObject> objects = null;
    public GameObject Sablon;

    [MinMaxSlider(0,10)]
    public Vector2 Speed = new Vector2(0.5f, 3f);

    [MinMaxSlider(0, 5)]
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


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForAmplitudeMChange());
        StartCoroutine(WaitForSppeedMChange());
    }

    // Update is called once per frame
    void Update()
    {

        if (IsRunning && objects == null)
        {
            this.Generate();
        }
        if (!IsRunning && objects != null)
        {
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            objects = null;
        }
    }

    public void Generate()
    {
        objects = new List<GameObject>();
        Random.InitState(seed);
        for (int i = 0; i < NrOfObjects; i++)
        {
            CreateObject(i);
        }
    }

    private void CreateObject(int id)
    {

        GameObject gameObject = Instantiate(Sablon, transform);
        gameObject.name = (id + 1).ToString();
        objects.Add(gameObject);
        var instance = (Movement)objects[id].GetComponent(typeof(Movement));

        instance.speed = Random.Range(Speed.x, Speed.y);
        instance.amplitude = Random.Range(Amplitude.x, Amplitude.y);

        instance.elipsedX = Random.Range(ElipsedX.x, ElipsedX.y);
        while ((instance.elipsedX < 0.3 && instance.elipsedX > -0.3) || (-2 < instance.amplitude * instance.elipsedX && instance.amplitude * instance.elipsedX < 2))
        {
            instance.elipsedX = Random.Range(ElipsedX.x, ElipsedX.y);
        }

        instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
        while ((instance.elipsedZ < 0.3 && instance.elipsedZ > -0.3) || (-2 < instance.amplitude * instance.elipsedZ && instance.amplitude * instance.elipsedZ < 2))
        {
            instance.elipsedZ = Random.Range(ElipsedZ.x, ElipsedZ.y);
        }

        instance.defaultSpeed = instance.speed;
        instance.defaultAmplitude = instance.amplitude;
        instance.speed *= SpeedMultiplier;
        instance.amplitude *= AmplitudeMultiplier;
        instance.defaultAlt = Random.Range(DefaultAlt.x, DefaultAlt.y);
        instance.verticalDelta = Random.Range(VerticalDelta.x, VerticalDelta.y);
        instance.offsetInMicros = Random.Range(OffsetInMicros.x, OffsetInMicros.y);

        float scale = Random.Range(Scale.x, Scale.y);
        if ((int)meshType == 1)
        {
            scale += 3f;
        }
        else if ((int)meshType == 2)
        {
            scale *= 200;
        }
        instance.transform.localScale = new Vector3(scale, scale, scale);

        instance.Randomizer((int)meshType);
        instance.StartMovement();
    }

    private void AddObjects(int NrObj)
    {
        if (objects != null)
        {
            for (int i = 0; i < NrObj; i++)
            {
                CreateObject(NrOfObjects);
                NrOfObjects++;
            }
        }
    }

    private void RemoveObjects(int NrObj)
    {
        for (int i = 0; i < NrObj; i++)
        {
            Destroy(objects[objects.Count - 1]);
            objects.RemoveAt(objects.Count - 1);
            NrOfObjects=objects.Count;
        }
    }

    public IEnumerator WaitForSppeedMChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => SpeedMultiplier!=previousSpeedMultiplier);
            ChangeSpeed(SpeedMultiplier);
        }
    }

    public IEnumerator WaitForAmplitudeMChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => AmplitudeMultiplier != previousAmplitudeMultiplier);
            ChangeAmplitude(AmplitudeMultiplier);
        }
    }

    public void Play(int nrOfObjects, float speed, float amplitude, MeshTypes meshType, int seed)
    {
        this.NrOfObjects = nrOfObjects;
        this.meshType = meshType;
        this.seed = seed;
        this.SpeedMultiplier = speed;
        this.AmplitudeMultiplier = amplitude;
        IsRunning = !IsRunning;
    }

    public void ChangeNumberOfObjects(float numberOfObjects)
    {
        if (this.NrOfObjects-numberOfObjects<0)
        {
            AddObjects((int)Mathf.Abs(this.NrOfObjects - numberOfObjects));
        }
        else
        {
            RemoveObjects((int)Mathf.Abs(this.NrOfObjects - numberOfObjects));
        }
    }

    public void ChangeSpeed(float speed)
    {
        if(IsRunning)
        foreach (GameObject obj in objects)
        {
            var instance = (Movement)obj.GetComponent(typeof(Movement));
            instance.speed = instance.defaultSpeed * speed;
        }
    }

    public void ChangeAmplitude(float amplitudeMultiplier)
    {
        if (IsRunning)
            foreach (GameObject obj in objects)
            {
                var instance = (Movement)obj.GetComponent(typeof(Movement));
                instance.amplitude = instance.defaultAmplitude * amplitudeMultiplier;
            }
    }


    public void ChangeRadius(float radius)
    {
        Amplitude *= radius;
    }
}
