using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //Crosshairs for Grappling
    public Image grappling_crosshair;
    public Image default_crosshair;
    public int flySpeed = 25;
    public LayerMask layerMask;
    public float airResist = 0.8f;
    private bool validGrappleTarget = false;
    private Vector3 flyToPoint;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isGrappling = Input.GetKey(KeyCode.F);
        bool isGrounded = characterController.isGrounded;

        float curSpeedX;
        float curSpeedY;

        if (isGrounded)
        {
            curSpeedX = canMove ? ((isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical")) : 0;
            curSpeedY = canMove ? ((isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal")) : 0;

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        }

        float movementDirectionY = moveDirection.y;
        

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            moveDirection.x *= airResist;
            moveDirection.z *= airResist;
        }
        

        //Flying Element

        Vector3 point = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(point);

        RaycastHit fly_hit;

        bool isClimbable = Physics.Raycast(ray, out fly_hit, Mathf.Infinity, layerMask);
        if (isClimbable) //<-- Add layerMask element here!
        {
            grappling_crosshair.enabled = true;
            default_crosshair.enabled = false;
        }
        else
        {
            grappling_crosshair.enabled = false;
            default_crosshair.enabled = true;

        }

        //Assign the grapple target and prevent any changes until key is pressed again
        if (Input.GetKeyDown("f") && isClimbable)
        {
            flyToPoint = fly_hit.point;
            validGrappleTarget = true;
        }
        //Calculate the movment vector between current pos and target
        if (Input.GetKey("f") && isClimbable && validGrappleTarget)
        {
            //Calculate movement Vector
            Vector3 playerPosition = transform.position;
            Vector3 flyDirection = (flyToPoint - playerPosition).normalized;
            flyDirection *= flySpeed;
            moveDirection = flyDirection;
        }
        else
        {
            moveDirection = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        }

        //Reset the grapple target on key up
        if(Input.GetKeyUp("f"))
        {
            validGrappleTarget = false;
            flyToPoint = Vector3.zero;
        }


        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}