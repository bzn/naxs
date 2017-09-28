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
        view1Button.onClick.AddListener(ViewButton1OnClick);
        view2Button.onClick.AddListener(ViewButton2OnClick);
        view3Button.onClick.AddListener(ViewButton3OnClick);
        view4Button.onClick.AddListener(ViewButton4OnClick);
        view5Button.onClick.AddListener(ViewButton5OnClick);
        view6Button.onClick.AddListener(ViewButton6OnClick);
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

    private void ViewButton1OnClick()
    {
        ViewButtonOnClick(1);
    }

    private void ViewButton2OnClick()
    {
        ViewButtonOnClick(2);
    }

    private void ViewButton3OnClick()
    {
        ViewButtonOnClick(3);
    }

    private void ViewButton4OnClick()
    {
        ViewButtonOnClick(4);
    }

    private void ViewButton5OnClick()
    {
        ViewButtonOnClick(5);
    }

    private void ViewButton6OnClick()
    {
        ViewButtonOnClick(6);
    }
}