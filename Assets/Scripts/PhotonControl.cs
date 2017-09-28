﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;    
    public Text logText;
    public Text pingText;

    void Start()
    {
        string path = Application.dataPath + "//DeviceID.txt";
        if (File.Exists(path))
        {
            StreamReader sr = File.OpenText(path);
            string input = "";
            input = sr.ReadLine();
            PlayerDataControl.instance.deviceID = int.Parse(input);
            SetTestText("device ID=" + input);
            sr.Close();
        }
        else
        {
            SetTestText("Can't find "+ path);
        }

        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        PhotonNetwork.automaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings("v1.0");
        PlayerDataControl.instance.UpdateStatusStart();
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
        PhotonNetwork.playerName = PlayerDataControl.instance.deviceID.ToString();

        PhotonNetwork.JoinOrCreateRoom("MainRoom", options, TypedLobby.Default);
    }    

    public override void OnJoinedRoom()
    {
        Debug.Log("Enter Game Room");

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("Scene1");
        }

        if (PlayerDataControl.instance.deviceID == 0)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.player);
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

    public void SetTestText(string str)
    {
        logText.text = str;
    }

    public void AddTestText(string str)
    {
        logText.text += "\n"+str;
    }

    public void SetPingText(string str)
    {
        pingText.text = str;
    } 
}

