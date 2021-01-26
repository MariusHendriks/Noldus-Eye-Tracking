using RogoDigital.Lipsync;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

// A class to help in creating the Json object to be sent to the rasa server
public class PostMessageJson
{
    public string message;
    public string sender;
}

[Serializable]
// A class to extract multiple json objects nested inside a value
public class RootReceiveMessageJson
{
    public ReceiveMessageJson[] messages;
}

[Serializable]
// A class to extract a single message returned from the bot
public class ReceiveMessageJson
{
    public string recipient_id;
    public string text;
    public string image;
    public string attachemnt;
    public string button;
    public string element;
    public string quick_replie;
}

public class NetworkManager : MonoBehaviour
{
    private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";
    private Dictionary<string, LipSyncData> rasaAnswers;

    public LipSyncData[] audioClips;
    private LipSync lipSync;

    private void Start()
    {
        rasaAnswers = new Dictionary<string, LipSyncData>
        {
            { "We are in a virtual representation of London.", audioClips[0] },
            { "There is a jewelry store, a cookie store, a yachting shop and a spa center.", audioClips[1] },
            { "The cars are on the normal side of the road.", audioClips[2] },
            { "You could buy something nice from the jewelry store, help yourself with a cookie from the cookie store, see some luxurious rides at the yachting shop or just relax at the spa.", audioClips[3] },
            { "Here is something to cheer you up. Not that you dirty mind!", audioClips[4] },
            { "Did that help you?", audioClips[5] },
            { "Great, to hear!", audioClips[6] },
            { "Bye", audioClips[7] },
            { "Piss off", audioClips[8] },
            { "I am a bot, powered by Rasa.", audioClips[9] },
            { "Hey! How are you?", audioClips[10] }
        };
        lipSync = GetComponent<LipSync>();
    }

    public void SendMessageToRasa(string msg)
    {
        // Create a json object from user message
        PostMessageJson postMessage = new PostMessageJson
        {
            sender = "user",
            message = msg
        };

        string jsonBody = JsonUtility.ToJson(postMessage);
        //print("User json : " + jsonBody);

        // Create a post request with the data to send to Rasa server
        StartCoroutine(PostRequest(rasa_url, jsonBody));
    }

    private IEnumerator PostRequest(string url, string jsonBody)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        RecieveResponse(request.downloadHandler.text);
    }

    // Parse the response received from the bot
    public void RecieveResponse(string response)
    {
        // Debug.Log(response);
        // Deserialize response recieved from the bot
        var recieveMessages =
            JsonUtility.FromJson<RootReceiveMessageJson>("{\"messages\":" + response + "}");

        // show message based on message type on UI
        foreach (ReceiveMessageJson message in recieveMessages.messages)
        {
            FieldInfo[] fields = typeof(ReceiveMessageJson).GetFields();
            foreach (FieldInfo field in fields)
            {
                string data = null;

                // extract data from response in try-catch for handling null exceptions
                try
                {
                    data = field.GetValue(message).ToString();
                }
                catch (NullReferenceException) { }

                // print data
                if (data != null && field.Name != "recipient_id")
                {
                    Debug.Log("Bot said \"" + data + "\"");
                    var audio = FindInDictionary(data);
                    TTSCallback(audio);
                }
            }
        }
    }

    private LipSyncData FindInDictionary(string message)
    {
        foreach (var item in rasaAnswers)
        {
            if (item.Key == message)
                return item.Value;
        }
        return null;
    }

    void TTSCallback(LipSyncData audio)
    {
        lipSync.Play(audio);
    }
}