using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataControl : MonoBehaviour
{
    public static PlayerDataControl instance;
    public float updateRate = 1.0f;
    public int deviceID = -1;
    public string status = "";
    public int ping = -1;
    public bool isHelmet = false;
    public bool isTracker = false;
    public int sceneID = -1;
    public Transform dummyTransform;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStatusStart()
    {
        InvokeRepeating("UpdateStatus", 0, updateRate);
    }

    private void UpdateStatus()
    {
        //string status = PhotonNetwork.connectionStateDetailed.ToString();
        int ping = PhotonNetwork.GetPing();
        PhotonControl.instance.SetPingText(status + "\n" + ping + " ms");

        // TODO (VIVE Status)
        // ....
        bool isHelmet = true;
        bool isTracker = true;        

        if (PhotonNetwork.inRoom)
        {
            GetComponent<PhotonView>().RPC("SyncStatus", PhotonTargets.All, deviceID, ping, isHelmet, isTracker, sceneID);
        }
    }

    [PunRPC]
    void SyncStatus(int id, int ping, bool isHelmet, bool isTracker, int sceneID)
    {
        if (id > 0)
        {
            if (GameMasterControl.instance.gameObject.activeSelf)
            {
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetPing(ping);
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetSceneID(sceneID);
            }
        }
    }
}
