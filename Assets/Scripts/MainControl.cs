using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour {
    private DummyControl dummyControl;
	// Use this for initialization
	void Start ()
    {
        if(PhotonNetwork.isMasterClient)
        {
            dummyControl = PhotonNetwork.Instantiate("Dummy", new Vector3(0, 0, 0), Quaternion.identity, 0).GetComponent<DummyControl>();
        }
        else
        {
            dummyControl = PhotonNetwork.Instantiate("Dummy", new Vector3(4, 0, 4), Quaternion.identity, 0).GetComponent<DummyControl>();
        }        
    }
}
