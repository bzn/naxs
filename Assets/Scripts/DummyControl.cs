using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
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

        headPos.transform.parent = MainControl.instance.dummysData.transform;
        headPos.transform.localPosition = new Vector3(0, 0, 0);
        headPos.transform.localRotation = Quaternion.identity;

        handLPos.transform.parent = MainControl.instance.dummysData.transform;
        handLPos.transform.localPosition = new Vector3(0, 0, 0);
        handLPos.transform.localRotation = Quaternion.identity;

        handRPos.transform.parent = MainControl.instance.dummysData.transform;
        handRPos.transform.localPosition = new Vector3(0, 0, 0);
        handRPos.transform.localRotation = Quaternion.identity;
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
}
