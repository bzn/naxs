using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewControl : MonoBehaviour
{
    private int deviceID = -1;
    private bool isHelmetOK = false;
    private bool isTrackerOK = false;

    public Text deviceIDText;
    public Text netStateText;
    public Text viveStateText;
    public Text sceneText;
    public Text pingText;

    void Awake()
    {

    }

    public void SetDeviceID(int id)
    {
        deviceID = id;
        deviceIDText.text = id.ToString();
    }

    public void SetNetState(string str)
    {
        netStateText.text = str;
    }

    public void SetViveState(bool isHelOK, bool isTraOK)
    {
        isHelmetOK = isHelOK;
        isTrackerOK = isTraOK;
        if (isHelOK)
        {
            viveStateText.text = "O / ";
        }
        else
        {
            viveStateText.text = "X / ";
        }

        if (isTraOK)
        {
            viveStateText.text += "O";
        }
        else
        {
            viveStateText.text += "X";
        }
    }

    public void SetSceneName(string str)
    {
        sceneText.text = str;
    }

    public void SetPing(int ping)
    {
        pingText.text = ping.ToString();
    }
}
