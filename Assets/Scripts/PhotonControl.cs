using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;    
    public int deviceID = -1;

    void Awake()
    {
        string path = Application.dataPath + "//DeviceID.txt";
        if (File.Exists(path))
        {
            StreamReader sr = File.OpenText(path);
            string input = "";
            input = sr.ReadLine();
            deviceID = int.Parse(input);
            Debug.Log("device ID=" + deviceID.ToString());
            sr.Close();
        }

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

