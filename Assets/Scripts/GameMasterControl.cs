using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterControl : MonoBehaviour
{
    public GameObject godCamera;
    public GameObject godCameraRoot;
    public Button godButton;
    public Button view1Button;
    public Button view2Button;
    public Button view3Button;
    public Button view4Button;
    public Button view5Button;
    public Button view6Button;
    public Button mainSceneButton;
    public Button scene2Button;
    public Button scene3Button;

    public static GameMasterControl instance;
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        godButton.onClick.AddListener(GodButtonOnClick);
        view1Button.onClick.AddListener(View1ButtonOnClick);
        view2Button.onClick.AddListener(View2ButtonOnClick);
        view3Button.onClick.AddListener(View3ButtonOnClick);
        view4Button.onClick.AddListener(View4ButtonOnClick);
        view5Button.onClick.AddListener(View5ButtonOnClick);
        view6Button.onClick.AddListener(View6ButtonOnClick);

        mainSceneButton.onClick.AddListener(mainSceneButtonOnClick);
        scene2Button.onClick.AddListener(scene2ButtonOnClick);
        scene3Button.onClick.AddListener(scene3ButtonOnClick);
    }

    private void SetGodCameraParent(GameObject parent)
    {
        godCamera.transform.parent = parent.transform;
        godCamera.transform.localPosition = new Vector3(0, 0, 0);
        godCamera.transform.localRotation = Quaternion.identity;        
    }

    private void GodButtonOnClick()
    {
        SetGodCameraParent(godCameraRoot);
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
                godCamera.transform.localEulerAngles = new Vector3(-90f, 0, 90f);
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

    private void mainSceneButtonOnClick()
    {
        PhotonNetwork.LoadLevel("MainScene");
        PhotonControl.instance.nowSceneID = 1;
    }

    private void scene2ButtonOnClick()
    {
        PhotonNetwork.LoadLevel("Scene2");
        PhotonControl.instance.nowSceneID = 2;
    }

    private void scene3ButtonOnClick()
    {
        PhotonNetwork.LoadLevel("Scene3");
        PhotonControl.instance.nowSceneID = 3;
    }
}