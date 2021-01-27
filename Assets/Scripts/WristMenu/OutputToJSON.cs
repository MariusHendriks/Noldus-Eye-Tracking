using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using PupilLabs;

public class OutputToJSON : MonoBehaviour
{
    public GazeController gazeController;
    private Vector3 localGazeDirection;
    private static List<OutputData> outputDatas = new List<OutputData>();

    private Transform mainCamera;
    private RaycastHit hitInfo;
    
    private Collider prevObjHit = null;
    private float collisionTimer = 0;
    private float simulationTimer = 0;
    public bool simulationWorking = false;

    private Vector3 startLocation = new Vector3();
    private Vector3 cameraStartLocation = new Vector3();


    public void OnDestroy()
    {
        string fileName = "OutputData-" + DateTime.Now.Date.Day+"-" + DateTime.Now.Date.Month + "-"+ DateTime.Now.Date.Year + ".json";
        string filePath = Application.persistentDataPath + "/OutputData/";

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        OutputDataCollection jsonFormat = new OutputDataCollection(fileName, DateTime.Now, outputDatas);
        string text = JsonUtility.ToJson(jsonFormat);
        File.WriteAllText(filePath + fileName, text);
        Debug.Log("Done");
    }

    public void AddOutputData(OutputData outputData) 
    {
        outputDatas.Add(outputData);
    }

    private void Start()
    {
        mainCamera = Camera.main.transform;

    }

    private void Update()
    {
        if(simulationWorking)
        {
            simulationTimer += Time.deltaTime;
        }
        else
        {
            simulationTimer = 0;
        }

        localGazeDirection = gazeController.GetComponentInChildren<GazeVisualizer>().GetGazeDirection();
        ReceiveGaze(localGazeDirection);
    }

    //TODO: Pass hitinfo data to Method AddOutputData
    void ReceiveGaze(Vector3 localGazeDirection)
    {
        Debug.DrawRay(mainCamera.position, mainCamera.TransformDirection(localGazeDirection) * 100.0f, Color.yellow);

        if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(localGazeDirection), out hitInfo, 100.0f))
        {

            if (hitInfo.collider.gameObject.layer == 22)
            {
                if (startLocation==new Vector3())
                {
                    startLocation = hitInfo.transform.position;
                    cameraStartLocation = mainCamera.transform.position;
                }

                if (prevObjHit == null)
                {
                    prevObjHit = hitInfo.collider;
                }


                if (hitInfo.collider == prevObjHit)
                {
                    collisionTimer += Time.deltaTime;
                }
                else
                {
                    Debug.Log("User looked at object " + prevObjHit + " for " + collisionTimer + " seconds.");
                                        
                    AddOutputData(new OutputData(prevObjHit.gameObject.name, startLocation, prevObjHit.transform.position, cameraStartLocation, mainCamera.transform.position, simulationTimer, collisionTimer));

                    prevObjHit = hitInfo.collider;
                    collisionTimer = 0;
                    startLocation = new Vector3();
                    cameraStartLocation = new Vector3();
                }
                  
            }
            else if (prevObjHit!= null)
            {
                Debug.Log("User looked at object " + prevObjHit + " for " + collisionTimer + " seconds.");
                AddOutputData(new OutputData(prevObjHit.gameObject.name, startLocation, prevObjHit.transform.position, cameraStartLocation, mainCamera.transform.position, simulationTimer, collisionTimer));
                prevObjHit = hitInfo.collider;
                collisionTimer = 0;
                startLocation = new Vector3();
                cameraStartLocation = new Vector3();
                prevObjHit = null;
            }
            
        }
    }

    [Serializable]
    public class OutputDataCollection
    {
        public string name;
        public string currentTime;
        public List<OutputData> outputData = new List<OutputData>();

        public OutputDataCollection(string name, DateTime currentTime, List<OutputData> outputData)
        {
            this.name = name;
            this.currentTime = currentTime.ToString();
            this.outputData = outputData;
        }
    }

    [Serializable]
    public class OutputData
    {
        public string name;
        public Vector3 startLocation;
        public Vector3 endLocation;

        public Vector3 cameraStartLocation;
        public Vector3 cameraEndLocation;

        public float timeSinceStartup;
        public float colisionDuration;

        public OutputData(string name, Vector3 startLocation, Vector3 endLocation, Vector3 cameraStartLocation, Vector3 cameraEndLocation, float timeSinceStartup, float colisionDuration)
        {
            this.name = name;
            
            this.startLocation = startLocation;
            this.endLocation = endLocation;

            this.cameraStartLocation = cameraStartLocation;
            this.cameraEndLocation = cameraEndLocation;

            this.colisionDuration = colisionDuration;
            this.timeSinceStartup = timeSinceStartup;
        }
    }
}
