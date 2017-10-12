using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerData_SetViveDevice : MonoBehaviour {

    public bool isValid { get; private set; }
    private IEnumerator coroutine;

    void OnEnable()
    {
        if (gameObject.name.Contains("Camera"))
        {
            newPosesAction.enabled = true;
            coroutine = WaitForCheckDeviceActivity(1.0f);
            StartCoroutine(coroutine);
        }
        else if(gameObject.name.Contains("Controller (right)"))
        {
            PlayerDataControl.instance.isTracker = true;
        }
    }

    void OnDisable()
    {
        if (gameObject.name.Contains("Camera"))
        {
            newPosesAction.enabled = false;
            StopCoroutine(coroutine);
        }
        else if (gameObject.name.Contains("Controller (right)"))
        {
            PlayerDataControl.instance.isTracker = false;
        }
    }

    private void OnNewPoses(TrackedDevicePose_t[] poses)
    {
        isValid = false;

        if (!poses[0].bDeviceIsConnected)
        {
            return;
        }

        if (!poses[0].bPoseIsValid)
        {
            return;
        }

        isValid = true;
    }

    SteamVR_Events.Action newPosesAction;

    PlayerData_SetViveDevice()
    {
        newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
    }

    IEnumerator WaitForCheckDeviceActivity(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            PlayerDataControl.instance.isHelmet = isValid;
        }
    }
}
