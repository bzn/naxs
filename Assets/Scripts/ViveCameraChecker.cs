using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveCameraChecker : MonoBehaviour {

    public bool isViveCameraValid;
    private IEnumerator coroutine;

    void OnEnable()
    {
        isViveCameraValid = false;
        if (PlayerDataControl.instance != null)
        {
            if (gameObject.name.Contains("Camera"))
            {
                newPosesAction.enabled = true;
            }
        }
    }

    void OnDisable()
    {
        if (PlayerDataControl.instance != null)
        {
            if (gameObject.name.Contains("Camera"))
            {
                newPosesAction.enabled = false;
            }
        }
    }

    private void OnNewPoses(TrackedDevicePose_t[] poses)
    {
        isViveCameraValid = false;
        
        int index = 0;//頭盔 index為0

        if (!poses[index].bDeviceIsConnected)
        {
            return;
        }

        if (!poses[index].bPoseIsValid)
        {
            return;
        }

        isViveCameraValid = true;
    }

    SteamVR_Events.Action newPosesAction;

    ViveCameraChecker()
    {
        newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
    }
}
