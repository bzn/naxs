using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;    
    public TextAsset deviceIDTxt;
    public int deviceID = -1;

    void Awake()
    {
        Debug.Log("device ID="+deviceIDTxt.text);
        deviceID = int.Parse(deviceIDTxt.text);

        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Master Server Connected");
        JoinGameRoom();
    }

    public void JoinGameRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("MainRoom", options, TypedLobby.Default);
    }    

    public override void OnJoinedRoom()
    {
        Debug.Log("Enter Game Room");

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("MainScene");
        }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom)
        {
            // Error
            // ....
            return;
        }
        Debug.Log("Enter Scene");
    }
}

