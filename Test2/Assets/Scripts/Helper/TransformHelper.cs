using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHelper : MonoBehaviour
{
    public static Transform GetTransformChild(string name,Transform root)
    {
        Transform[] t = root.GetComponentsInChildren<Transform>();
        foreach (var item in t)
        {
            if(item.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
