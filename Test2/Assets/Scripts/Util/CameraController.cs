using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public CameraMode currentMode = CameraMode.TopDown;
    public Transform rig;
    public GameObject topDown, thirdPerson;
    [HideInInspector]
    public GameObject currentCamera;
    [HideInInspector]
    public Transform target;
    private Quaternion origin;
    public enum CameraMode
    {
        TopDown=1,
        ThirdPerson=2
    }
    private void Start()
    {
        origin = transform.rotation;
        target = Character.main.transform;
        SwitchCamera(currentMode);
    }
    private void Update()
    {
        transform.position = target.position;
    }
    public void ResetCamera()
    {
        transform.rotation = origin;
        rig.rotation = Quaternion.identity;
    }
    public void SwitchCamera(CameraMode mode)
    {
        currentMode = mode;
        switch (mode)
        {
            case CameraMode.TopDown:
                ResetCamera();
                topDown.SetActive(true);
                thirdPerson.SetActive(false);
                currentCamera = topDown;
                UIController.Instance.crosshair.SetActive(false);
                break;
            case CameraMode.ThirdPerson:
                transform.rotation = target.rotation;
                topDown.SetActive(false);
                thirdPerson.SetActive(true);
                currentCamera = thirdPerson;
                UIController.Instance.crosshair.SetActive(true);
                break;
        }
    }
    public void LimitRotation(float min, float max)
    {
        Quaternion rot = rig.localRotation;
        if (rot.x > max)
        {
            rot.x = max;
        }
        if (rot.x < min)
        {
            rot.x = min;
        }
        rig.localRotation = rot;
    }
}
