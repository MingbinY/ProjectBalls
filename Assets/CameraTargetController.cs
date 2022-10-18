using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetController : MonoBehaviour
{
    public Vector2 turn;
    public float mouseSensetivity = .5f;
    public float lookAngle;
    public float pivotAngle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        turn.x = Input.GetAxis("Mouse X") * mouseSensetivity;

        
    }

    public void RotateCamera(Vector2 rVector)
    {
        lookAngle = lookAngle + (rVector.x * mouseSensetivity);
    }
}
