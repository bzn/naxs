using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;    
    public int deviceID = -1;
    public Text testText;

    void Awake()
    {
        string path = Application.dataPath + "//DeviceID.txt";
        if (File.Exists(path))
        {
            StreamReader sr = File.OpenText(path);
            string input = "";
            input = sr.ReadLine();
            deviceID = int.Parse(input);
            SetTestText("device ID=" + deviceID.ToString());
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
        PhotonNetwork.playerName = deviceID.ToString();

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

    public void SetTestText(string str)
    {
        testText.text = str;
    }

    public void AddTestText(string str)
    {
        testText.text += "\n"+str;
    }
}

