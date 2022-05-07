using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public bool useKeyboard;
    public GameObject crosshair;
    public GameObject endUI;
    private void Update()
    {
        if (useKeyboard)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            MoveCharacter(input);
        }
    }
    public void SwitchCamera()
    {
        int current = (int)CameraController.Instance.currentMode;
        current++;
        current = current > 2 ? 1 : current;
        CameraController.Instance.SwitchCamera((CameraController.CameraMode)current);
    }
    public void StopCharacter()
    {
        MoveCharacter(Vector2.zero);
    }
    public void MoveCharacter(Vector2 move)
    {
        Transform cam = CameraController.Instance.currentCamera.transform;
        Vector3 dir = (cam.forward*move.y) + (cam.right*move.x);
        dir.y = 0;
        Character.main.Move(dir);
    }
    public void StopShoot()
    {
        Character.main.Shoot(Vector3.zero,false);
    }
    public void ShootCharacter(Vector2 move)
    {
        Transform cam = CameraController.Instance.currentCamera.transform;
        Vector3 dir = (cam.forward * move.y) + (cam.right * move.x);
        dir.y = 0;
        if(CameraController.Instance.currentMode == CameraController.CameraMode.TopDown)
        {
            Character.main.Shoot(dir,true);
        }else if(CameraController.Instance.currentMode == CameraController.CameraMode.ThirdPerson)
        {
            if (move != Vector2.zero)
            {
                CameraController.Instance.transform.Rotate(Vector3.up, move.x*0.5f, Space.World);
                CameraController.Instance.rig.Rotate(Vector3.right, -move.y * 0.5f, Space.Self);
                CameraController.Instance.LimitRotation(-0.4f, 0.4f);
                Character.main.shootPoint.rotation = Quaternion.identity;
            }
            Character.main.Shoot(CameraController.Instance.rig.forward, true);
            Character.main.transform.rotation = CameraController.Instance.rig.rotation;
            if(Physics.Raycast(cam.position,cam.forward,out RaycastHit hit))
            {
                Character.main.shootPoint.LookAt(hit.point);
            }
            else
            {
                Character.main.shootPoint.LookAt(cam.position+cam.forward*50);
            }

        }
    }
    public void OnGameEnd()
    {
        endUI.SetActive(true);
    }
}
