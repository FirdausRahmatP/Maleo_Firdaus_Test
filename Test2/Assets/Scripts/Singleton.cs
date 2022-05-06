using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    private void Awake()
    {
        SingletonInit();
    }
    public void SingletonInit()
    {
        Instance = this as T;
    }
}
