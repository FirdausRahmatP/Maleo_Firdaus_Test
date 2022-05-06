using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public CameraMode currentMode = CameraMode.TopDown;
    public GameObject topDown, thirdPerson;
    [HideInInspector]
    public GameObject currentCamera;
    [HideInInspector]
    public Transform target;
    public enum CameraMode
    {
        TopDown=1,
        ThirdPerson=2
    }
    private void Start()
    {
        target = Character.main.transform;
        SwitchCamera(currentMode);
    }
    private void Update()
    {
        transform.position = target.position;
    }
    public void SwitchCamera(CameraMode mode)
    {
        currentMode = mode;
        switch (mode)
        {
            case CameraMode.TopDown:
                topDown.SetActive(true);
                thirdPerson.SetActive(false);
                currentCamera = topDown;
                break;
            case CameraMode.ThirdPerson:
                topDown.SetActive(false);
                thirdPerson.SetActive(true);
                currentCamera = thirdPerson;
                break;
        }
    }
}
