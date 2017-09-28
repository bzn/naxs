using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
