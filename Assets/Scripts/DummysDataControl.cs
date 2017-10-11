using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 掌握所有Dummy的Helmet和Tracker的座標資料

public class DummysDataControl : MonoBehaviour
{
    public static DummysDataControl instance;
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
}
