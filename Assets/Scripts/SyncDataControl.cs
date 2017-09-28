using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncDataControl : MonoBehaviour
{
    public static SyncDataControl instance;
    public float updateRate = 1.0f;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("UpdateStatus", 0, updateRate);
    }

    private void UpdateStatus()
    {
        string status = PhotonNetwork.connectionStateDetailed.ToString();
        int ping = PhotonNetwork.GetPing();

        // TODO (VIVE Status)
        // ....
        bool isHelmet = true;
        bool isTracker = true;

        int sceneID = MainControl.instance.nowSceneID;

        if (PhotonNetwork.inRoom)
        {
            GetComponent<PhotonView>().RPC("SyncStatus", PhotonTargets.All, PhotonControl.instance.deviceID, status, ping, isHelmet, isTracker, sceneID);
        }
    }

    [PunRPC]
    void SyncStatus(int id, string status, int ping, bool isHelmet, bool isTracker, int sceneID)
    {
        if (id > 0)
        {
            if (MainControl.instance.gameMasterControl.activeSelf)
            {
                MainControl.instance.playerViewsControl.playerViewControl[id - 1].SetNetState(status);
                MainControl.instance.playerViewsControl.playerViewControl[id - 1].SetPing(ping);
                MainControl.instance.playerViewsControl.playerViewControl[id - 1].SetSceneID(sceneID);
            }
        }
    }
}
