using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckViveControllerRight : MonoBehaviour {

    public bool isViveControllerRight;

    void OnEnable()
    {
        isViveControllerRight = false;
        if (PlayerDataControl.instance != null)
        {
            if (gameObject.name.Contains("Controller (right)"))
            {
                isViveControllerRight = true;
            }
        }
    }

    void OnDisable()
    {
        if (PlayerDataControl.instance != null)
        {
            if (gameObject.name.Contains("Controller (right)"))
            {
                isViveControllerRight = false;
            }
        }
    }
}
