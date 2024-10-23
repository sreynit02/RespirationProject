using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp.Server; // Import WebSocketSharp
using WebSocketSharp;
using UnityEditor.Scripting.Python;

public class UnityWebocketServer : MonoBehaviour
{
    private WebSocketServer wssServer;


    // Start is called before the first frame update
    void Start()
    {
        wssServer = new WebSocketServer("ws://localhost:8000");

        wssServer.AddWebSocketService<BreathingDataHandler>("/data");

        wssServer.Start();

        if (wssServer.IsListening)
        {
            Debug.Log($"WebSocket server started at ws://localhost:8000");

            // Run the Python script after starting the server
            //PythonRunner.RunFile("Assets/CSharp-Scripts/new_python_script.py");
            // need to use websharp for C#
        }
    }
    private void OnApplicationQuit()
    {
        if (wssServer != null)
        {
            wssServer.Stop();
            wssServer = null;
        }
    }
    
}


public class BreathingDataHandler : WebSocketBehavior
{
    public static BreathingDataHandler Instance { get; private set; }

    public BreathingData breathingData;

    public BreathingDataHandler()
    {
        Instance = this;
        breathingData = new BreathingData();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        breathingData = JsonUtility.FromJson<BreathingData>(e.Data);
        Debug.Log("Value Recieved: " + breathingData.value);
    }
}

[Serializable]
public class BreathingData
{
    //public string time;
    public float value;
}