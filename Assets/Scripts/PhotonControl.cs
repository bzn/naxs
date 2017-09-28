using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;
    public float updateRate = 1.0f;
    public int deviceID = -1;
    public Text logText;
    public Text pingText;

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
        InvokeRepeating("UpdateStatus", 0, updateRate);
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
        logText.text = str;
    }

    public void AddTestText(string str)
    {
        logText.text += "\n"+str;
    }

    private void UpdateStatus()
    {
        string status = PhotonNetwork.connectionStateDetailed.ToString();
        int ping = PhotonNetwork.GetPing();
        SetPingText(status + "\n" + ping + " ms");

        // TODO (VIVE Status)
        // ....
        bool isHelmet = true;
        bool isTracker = true;

        // TODO (Scene)
        // ....
        int sceneID = 1;

        if (PhotonNetwork.inRoom)
        {
            GetComponent<PhotonView>().RPC("SyncStatus", PhotonTargets.All, deviceID, status, ping, isHelmet, isTracker, sceneID);
        }            
    }

    private void SetPingText(string str)
    {
        pingText.text = str;
    }

    [PunRPC]
    void SyncStatus(int id, string status, int ping, bool isHelmet, bool isTracker, int sceneID)
    {
        if (id > 0)
        {         
            if(MainControl.instance.gameMasterControl.activeSelf)
            {
                MainControl.instance.playerViewsControl.playerViewControl[id - 1].SetNetState(status);
                MainControl.instance.playerViewsControl.playerViewControl[id - 1].SetPing(ping);
            }            
        }
    }
}

