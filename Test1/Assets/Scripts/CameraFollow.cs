using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    public void Follow()
    {
        if (target == null)
        {
            target = FindObjectOfType<AICharacter>().transform;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.2f);
        }
    }
}
