using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LogFileCreator : MonoBehaviour
{
    public string SceneName { get; private set; }
    private readonly string path;

    public LogFileCreator(string sceneName)
    {
        SceneName = sceneName;
        path = $"{Application.dataPath}/{SceneName}Log.txt";
    }

    public void CreateTextFile()
    {
        if (!File.Exists(path))
            File.WriteAllText(path, $"{SceneName} scene log: {DateTime.Now}");
    }

    public void AppendTextToTextFile(string content)
    {
        File.AppendAllText(path, content);
    }
     
}
