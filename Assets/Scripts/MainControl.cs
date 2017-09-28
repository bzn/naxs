using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainControl : MonoBehaviour
{
    public GameObject cameraPos;
    public GameObject controllerLPos;
    public GameObject controllerRPos;
    public GameObject dummysData;
    public static MainControl instance;

    //public GameObject gameMasterControl;
    public GameObject cameraRig;

    private GameObject dummyGO;

    //public PlayerViewsControl playerViewsControl;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        if (PhotonNetwork.inRoom)
        {
            if(PlayerDataControl.instance.deviceID == 0)
            {
                GameMasterControl.instance.gameObject.SetActive(true);
                cameraRig.SetActive(false);
            }
            else
            {
                GameMasterControl.instance.gameObject.SetActive(false);
                cameraRig.SetActive(true);
                Debug.Log("PID" + PhotonNetwork.player.ID);
                
                dummyGO = PhotonNetwork.Instantiate("Dummy", new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
            }

            // blues debug
            GameMasterControl.instance.gameObject.SetActive(true);
        }
        else
        {
            // Offline Test
            dummyGO = Instantiate(Resources.Load("Dummy", typeof(GameObject))) as GameObject;
        }        
    }

    void OnDestroy()
    {
        PhotonNetwork.Destroy(dummyGO);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("Player Connected = " + player.NickName);
        int id = int.Parse(player.NickName);
        if (id > 0)
        {
            GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetNetState("O");
        }
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected =" + player.NickName);
        int id = int.Parse(player.NickName);
        if(id > 0)
        {
            GameMasterControl.instance.playerViewsControl.playerViewControl[id - 1].SetNetState("-");
        }
    }
}
