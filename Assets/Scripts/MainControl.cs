using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainControl : MonoBehaviour
{
    public GameObject cameraPos;
    public GameObject controllerLPos;
    public GameObject controllerRPos;
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

    }

    void Start ()
    {
        if (PhotonNetwork.inRoom)
        {
            if(PlayerDataControl.instance.deviceID == 0)
            {
                GameMasterControl.instance.gameObject.SetActive(true);
            }
            else
            {
                GameMasterControl.instance.gameObject.SetActive(false);
                Debug.Log("PID" + PhotonNetwork.player.ID);
                
                dummyGO = PhotonNetwork.Instantiate("Dummy", new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
            }
        }
        else
        {
            // Offline Test
            dummyGO = Instantiate(Resources.Load("Dummy", typeof(GameObject))) as GameObject;
        }        
    }

    void OnDestroy()
    {
        if(PhotonNetwork.isMasterClient)
        {
            Debug.Log("AAAA");
            PhotonNetwork.DestroyAll();
        }        
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
