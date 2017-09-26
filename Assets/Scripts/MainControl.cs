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

    public GameObject gameMasterControl;
    public GameObject cameraRig;

    public PlayerViewsControl playerViewsControl;

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
            if(PhotonControl.instance.deviceID == 0)
            {
                gameMasterControl.SetActive(true);
                cameraRig.SetActive(false);
            }
            else
            {
                gameMasterControl.SetActive(false);
                cameraRig.SetActive(true);
                Debug.Log("PID" + PhotonNetwork.player.ID);
                
                GameObject dummyGO = PhotonNetwork.Instantiate("Dummy", new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
            }

            // blues debug
            gameMasterControl.SetActive(true);
        }
        else
        {
            // Offline Test
            GameObject dummyGO = Instantiate(Resources.Load("Dummy", typeof(GameObject))) as GameObject;
        }        
    }

    void Update()
    {
        // delay
        // ....

        GameObject[] headGOs = GameObject.FindGameObjectsWithTag("HeadPos");
        GameObject[] handLGOs = GameObject.FindGameObjectsWithTag("HandLPos");
        GameObject[] handRGOs = GameObject.FindGameObjectsWithTag("HandRPos");

        Debug.Log("headGOs.Length=" + headGOs.Length.ToString());
        for (int i = 0; i < headGOs.Length; i++)
        {
            int id = int.Parse(headGOs[i].GetComponent<PhotonView>().owner.NickName);
            playerViewsControl.playerViewControl[id - 1].SetPosText(headGOs[i].transform.position);
        }
    }
}
