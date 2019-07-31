using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class NewBehaviourScript : MonoBehaviour
{

    NetworkClient myClient;
    public bool isAtStartup = true;


    // Use this for initialization
    void Start()
    {
//        SetupServer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        isAtStartup = false;
    }

    public void SetupClient()
    {

        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
        isAtStartup = false;
    }

    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        isAtStartup = false;
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }


}
