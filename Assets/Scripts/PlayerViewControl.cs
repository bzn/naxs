using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewControl : MonoBehaviour
{
    private int deviceID = -1;
    private bool isNetOK = false;
    private bool isHelmetOK = false;
    private bool isTrackerOK = false;
    private int sceneID = -1;
    //private string eventString = "";
    private string posString = "";

    public Text deviceIDText;
    public Text netStateText;
    public Text viveStateText;
    public Text sceneText;
    //public Text eventText;
    public Text posText;

    void Awake()
    {

    }

    public void SetDeviceID(int id)
    {
        deviceID = id;
        deviceIDText.text = id.ToString();
    }

    public void SetNetState(bool isOK)
    {
        isNetOK = isOK;
        if(isOK)
        {
            netStateText.text = "O";
        }
        else
        {
            netStateText.text = "X";
        }
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

    public void SetSceneID(int id)
    {
        sceneID = id;
        sceneText.text = id.ToString();
    }

    public void SetPosText(Vector3 v3)
    {
        posString = "(" + v3.x + "," + v3.y + "," + v3.z + ")";
        posText.text = posString;
    }
}
