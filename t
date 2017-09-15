[1mdiff --git a/Assets/Scripts/DummyControl.cs b/Assets/Scripts/DummyControl.cs[m
[1mindex ebc9fdd..09ec393 100644[m
[1m--- a/Assets/Scripts/DummyControl.cs[m
[1m+++ b/Assets/Scripts/DummyControl.cs[m
[36m@@ -6,7 +6,7 @@[m [mpublic class DummyControl : MonoBehaviour[m
 {[m
     public GameObject headPos;    [m
     public GameObject handLPos;[m
[31m-    public GameObject handRPos;[m
[32m+[m[32m    public GameObject handRPos;[m[41m    [m
     private PhotonView photonView;[m
     void Start()[m
     {[m
[36m@@ -28,11 +28,15 @@[m [mpublic class DummyControl : MonoBehaviour[m
         handRPos.transform.parent = MainControl.instance.dummysData.transform;[m
         handRPos.transform.localPosition = new Vector3(0, 0, 0);[m
         handRPos.transform.localRotation = Quaternion.identity;[m
[32m+[m[41m[m
[32m+[m[32m        Debug.Log("photonView.isMine=" + photonView.isMine.ToString());[m[41m[m
[32m+[m[32m        Debug.Log("PhotonNetwork.inRoom=" + PhotonNetwork.inRoom.ToString());[m[41m[m
[32m+[m[32m        Debug.Log("photonView.viewID=" + photonView.viewID.ToString());[m[41m[m
     }[m
 [m
     void Update()[m
[31m-    {[m
[31m-        if (photonView.isMine || !PhotonNetwork.inRoom)[m
[32m+[m[32m    {[m[41m        [m
[32m+[m[32m        if (photonView.isMine)[m[41m[m
         {[m
             headPos.transform.position = MainControl.instance.cameraPos.transform.position;[m
             headPos.transform.rotation = MainControl.instance.cameraPos.transform.rotation;[m
