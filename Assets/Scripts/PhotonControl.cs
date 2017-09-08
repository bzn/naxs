using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonControl : Photon.PunBehaviour
{
    public static PhotonControl instance;
    public static GameObject localPlayer;

    void Awake()
    {
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
        // 連上 MasterServer 後,  Button (UI) 就可以顯示或是執行其它後續動作.
        Debug.Log("已連上 Master Server");
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
        Debug.Log("您已進入遊戲室!!");
        // 如果是Master Client, 即可建立/初始化,與載入遊戲場景
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("MainScene");
        }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        // 若不在Photon的遊戲室內, 則網路有問題..
        if (!PhotonNetwork.inRoom)
        {
            return;
        }
        Debug.Log("我們已進入遊戲場景了,耶~");        
    }
}

