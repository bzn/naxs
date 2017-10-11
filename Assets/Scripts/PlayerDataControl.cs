using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Player應廣播給其他人的所有資料

public class PlayerDataControl : MonoBehaviour
{
    public static PlayerDataControl instance;
    public float updateRate = 1.0f;
    public int deviceID = -1;
    public string status = "";
    public int ping = -1;
    public bool isHelmet = false;
    public bool isTracker = false;    

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
        string status = PhotonNetwork.connectionStateDetailed.ToString();
        int ping = PhotonNetwork.GetPing();        

        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        if (PhotonNetwork.inRoom)
        {
            GetComponent<PhotonView>().RPC("SyncStatus", PhotonTargets.All, deviceID, status, ping, isHelmet, isTracker, sceneName);
        }
    }

    [PunRPC]
    void SyncStatus(int id, string status, int ping, bool isHelmet, bool isTracker, string sceneName)
    {
        if (id > 0)
        {
            if (GameMasterControl.instance.gameObject.activeSelf)
            {
                if(status == "Joined")
                {
                    status = "O";
                }
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetNetState(status);
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetPing(ping);
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetViveState(isHelmet, isTracker);
                GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetSceneName(sceneName);
            }
        }
    }
}
