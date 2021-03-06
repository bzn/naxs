﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

// GameMaster相關功能

public class GameMasterControl : MonoBehaviour
{
    public GameObject gmCamera;
    public GameObject gmCameraRoot;
    public Button gmButton;
    public Button view1Button;
    public Button view2Button;
    public Button view3Button;
    public Button view4Button;
    public Button view5Button;
    public Button view6Button;
    public Button mainSceneButton;
    public Button scene2Button;
    public Button scene3Button;
    public Button cameraTurnOnButton;
    public Button cameraTurnOffButton;
    public PlayerViewsControl playerViewsControl;
    public static GameMasterControl instance;
    //
    public bool isTronMode;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        isTronMode = false;
    }

    void Start()
    {
        gmButton.onClick.AddListener(GMButtonOnClick);
        view1Button.onClick.AddListener(View1ButtonOnClick);
        view2Button.onClick.AddListener(View2ButtonOnClick);
        view3Button.onClick.AddListener(View3ButtonOnClick);
        view4Button.onClick.AddListener(View4ButtonOnClick);
        view5Button.onClick.AddListener(View5ButtonOnClick);
        view6Button.onClick.AddListener(View6ButtonOnClick);

        mainSceneButton.onClick.AddListener(MainSceneButtonOnClick);
        scene2Button.onClick.AddListener(Scene2ButtonOnClick);
        scene3Button.onClick.AddListener(Scene3ButtonOnClick);
        cameraTurnOnButton.onClick.AddListener(CameraTurnOnButtonOnClick);
        cameraTurnOffButton.onClick.AddListener(CameraTurnOffButtonOnClick);
    }

    private void SetGodCameraParent(GameObject parent)
    {
        gmCamera.transform.parent = parent.transform;
        gmCamera.transform.localPosition = new Vector3(0, 0, 0);
        gmCamera.transform.localRotation = Quaternion.identity;        
    }

    private void GMButtonOnClick()
    {
        SetGodCameraParent(gmCameraRoot);
    }

    private void ViewButtonOnClick(int buttonID)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HeadPos");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int deviceID = int.Parse(gameObjects[i].GetComponent<PhotonView>().owner.NickName);
            if (deviceID == buttonID)
            {
                SetGodCameraParent(gameObjects[i]);
                gmCamera.transform.localEulerAngles = new Vector3(-90f, 0, 90f);
                break;
            }
        }
    }

    private void View1ButtonOnClick()
    {
        ViewButtonOnClick(1);
    }

    private void View2ButtonOnClick()
    {
        ViewButtonOnClick(2);
    }

    private void View3ButtonOnClick()
    {
        ViewButtonOnClick(3);
    }

    private void View4ButtonOnClick()
    {
        ViewButtonOnClick(4);
    }

    private void View5ButtonOnClick()
    {
        ViewButtonOnClick(5);
    }

    private void View6ButtonOnClick()
    {
        ViewButtonOnClick(6);
    }

    private void MainSceneButtonOnClick()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyAll();
            PhotonNetwork.LoadLevel("Scene1");
        }        
    }

    private void Scene2ButtonOnClick()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyAll();
            PhotonNetwork.LoadLevel("Scene2");
        }
    }

    private void Scene3ButtonOnClick()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyAll();
            PhotonNetwork.LoadLevel("Scene3");
        }
    }

    EVRSettingsError SetTronMode(bool enable)
    {
        EVRSettingsError e = EVRSettingsError.None;
        OpenVR.Settings.SetBool(OpenVR.k_pch_Camera_Section,
                                OpenVR.k_pch_Camera_EnableCameraForCollisionBounds_Bool,
                                enable,
                                ref e);
        OpenVR.Settings.SetInt32(OpenVR.k_pch_Camera_Section,
                                OpenVR.k_pch_Camera_BoundsColorGammaA_Int32,
                                0,
                                ref e);
        OpenVR.Settings.SetInt32(OpenVR.k_pch_Camera_Section,
                                OpenVR.k_pch_Camera_BoundsColorGammaR_Int32,
                                255,
                                ref e);
        OpenVR.Settings.SetInt32(OpenVR.k_pch_Camera_Section,
                                OpenVR.k_pch_Camera_BoundsColorGammaG_Int32,
                                255,
                                ref e);
        OpenVR.Settings.SetInt32(OpenVR.k_pch_Camera_Section,
                                OpenVR.k_pch_Camera_BoundsColorGammaB_Int32,
                                255,
                                ref e);
        OpenVR.Settings.Sync(true, ref e);
        return e;
    }

    private void CameraTurnOnButtonOnClick()
    {
        isTronMode = true;
        SendSetCameraTronMode();
    }

    private void CameraTurnOffButtonOnClick()
    {
        isTronMode = false;
        SendSetCameraTronMode();
    }

    private void SendSetCameraTronMode()
    {
        GetComponent<PhotonView>().RPC("SyncCamera", PhotonTargets.All, isTronMode);
    }

    [PunRPC]
    void SyncCamera(bool nowCameraMode)
    {
        if (PlayerDataControl.instance.deviceID > 0)
        {
            if (GameMasterControl.instance.gameObject.activeSelf)
            {
                SetTronMode(nowCameraMode);
            }
        }
    }    
}