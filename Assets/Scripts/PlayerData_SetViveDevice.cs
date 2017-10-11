using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_SetViveDevice : MonoBehaviour {

    void OnEnable()
    {
        if (gameObject.name.Contains("Camera"))
        {
            PlayerDataControl.instance.isHelmet = true;
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
            PlayerDataControl.instance.isHelmet = false;
        }
        else if (gameObject.name.Contains("Controller (right)"))
        {
            PlayerDataControl.instance.isTracker = false;
        }
    }
}
