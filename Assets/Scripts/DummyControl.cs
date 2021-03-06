﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

// 同步Helmet和Tracker的資料，更新至DummyData

public class DummyControl : MonoBehaviour
{
    public GameObject headPos;    
    public GameObject handLPos;
    public GameObject handRPos;    
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        VRIK vrik = GetComponent<VRIK>();
        vrik.solver.spine.headTarget = headPos.transform;
        vrik.solver.leftArm.target = handLPos.transform;
        vrik.solver.rightArm.target = handRPos.transform;

        headPos.transform.parent = DummysDataControl.instance.gameObject.transform;
        headPos.transform.localPosition = new Vector3(0, 0, 0);
        headPos.transform.localRotation = Quaternion.identity;

        handLPos.transform.parent = DummysDataControl.instance.gameObject.transform;
        handLPos.transform.localPosition = new Vector3(0, 0, 0);
        handLPos.transform.localRotation = Quaternion.identity;

        handRPos.transform.parent = DummysDataControl.instance.gameObject.transform;
        handRPos.transform.localPosition = new Vector3(0, 0, 0);
        handRPos.transform.localRotation = Quaternion.identity;

        Debug.Log("photonView.isMine=" + photonView.isMine.ToString());
        Debug.Log("PhotonNetwork.inRoom=" + PhotonNetwork.inRoom.ToString());
        Debug.Log("photonView.viewID=" + photonView.viewID.ToString());
    }

    void Update()
    {        
        if (photonView.isMine || !PhotonNetwork.inRoom)
        {
            headPos.transform.position = MainControl.instance.cameraPos.transform.position;
            headPos.transform.rotation = MainControl.instance.cameraPos.transform.rotation;
            handLPos.transform.position = MainControl.instance.controllerLPos.transform.position;
            handLPos.transform.rotation = MainControl.instance.controllerLPos.transform.rotation;
            handRPos.transform.position = MainControl.instance.controllerRPos.transform.position;
            handRPos.transform.rotation = MainControl.instance.controllerRPos.transform.rotation;
        }
    }

    public bool IsMine()
    {
        if(PlayerDataControl.instance.deviceID == int.Parse(GetComponent<PhotonView>().owner.NickName))
        {
            return true;
        }
        return false;
    }
}
