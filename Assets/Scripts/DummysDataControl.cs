using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummysDataControl : MonoBehaviour
{
    public PlayerViewsControl playerViewsControl;

    void Update ()
    {
        // delay
        // ....

        GameObject[] headGOs = GameObject.FindGameObjectsWithTag("HeadPos");
        GameObject[] handLGOs = GameObject.FindGameObjectsWithTag("HandLPos");
        GameObject[] handRGOs = GameObject.FindGameObjectsWithTag("HandRPos");

        Debug.Log("headGOs.Length=" + headGOs.Length.ToString());
        for (int i=0;i<headGOs.Length;i++)
        {
            int id = int.Parse(headGOs[i].GetComponent<PhotonView>().owner.NickName);
            playerViewsControl.playerViewControl[id - 1].SetPosText(headGOs[i].transform.position);
        }
    }
}
