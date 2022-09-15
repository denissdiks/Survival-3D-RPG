using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 currentMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    //components
    private Rigidbody rig;

    private void Awake()
    {
        //assign the rigidbody at the begining of the game to rig variable
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;

        //assign it to rigidbody velocity
        rig.velocity = dir;
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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //if we are holding down A, W, S, D keys
        if(context.phase == InputActionPhase.Performed)
        {
            //move to direction depending on Vector2 value
            currentMovementInput = context.ReadValue<Vector2>();
        }
        //no longer pressing down any keys
        else if(context.phase == InputActionPhase.Canceled)
        {
            //no movement
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //is it the first frame we are presing the button?
        if(context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f), Vector3.down),
        };

        foreach (Ray ray in rays)
        {
            if(Physics.Raycast(ray, 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }
}
