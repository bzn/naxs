using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 進入每個Scene都會重新執行一次
// 連線完會在這裡產生Dummy

public class MainControl : MonoBehaviour
{
    public GameObject cameraPos;
    public GameObject controllerLPos;
    public GameObject controllerRPos;
    public static MainControl instance;
    public GameObject cameraRig;
    private GameObject dummyGO;
    //
    public CheckViveCamera checkViveCamera;
    public CheckViveControllerRight checkViveControllerRight;

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
        setViveCameraAndTracker();

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

    private void setViveCameraAndTracker()
    {
        PlayerDataControl.instance.checkViveCamera = checkViveCamera;
        PlayerDataControl.instance.checkViveControllerRight = checkViveControllerRight;
    }

    void OnDestroy()
    {
        if(PhotonNetwork.isMasterClient)
        {
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
