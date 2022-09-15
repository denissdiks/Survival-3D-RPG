using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private void LateUpdate()
    {
        CameraLook();
    }

    void CameraLook()
    {
        //Look up and down (mouse movement)
        camCurXRot += mouseDelta.y * lookSensitivity;

        //Clamp the value between min and max X
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        //apply to actual camera container
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        //rotate the player to left and right
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
