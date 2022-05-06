using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void SwitchCamera()
    {
        int current = (int)CameraController.Instance.currentMode;
        current++;
        current = current > 2 ? 1 : current;
        CameraController.Instance.SwitchCamera((CameraController.CameraMode)current);
    }
    public void MoveCharacter(Vector2 move)
    {
        Transform cam = CameraController.Instance.currentCamera.transform;
        Vector3 dir = (cam.forward*move.y) + (cam.right*move.x);
        dir.y = 0;
        Character.main.Move(dir);
    }
}
