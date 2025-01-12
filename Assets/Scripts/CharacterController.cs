using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (CharacterController))]


public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController characterController;
    public int playerSpeed = 10;
    private bool groundedPlayer;
    public float jumpHeight = 18.0f;
    public float gravityValue = -9.81f;
    private float terminalVelocity = -20.0f;
    private float distanceToGround;
    private float deltaY;
    private float vertSpeed;
    public int flySpeed = 25;
    public float sensitivityHor = 9.0f;
    public LayerMask layerMask;
    private Vector3 move;
    public int sprintBoost;
    private int baseSpeed;

    public Image grappling_crosshair;
    public Image default_crosshair;
    [SerializeField] GameObject canvas;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        canvas = GetComponent<GameObject>();

        //grappling_crosshair = canvas.transform.Find("grappling_crosshair");

        baseSpeed = playerSpeed;      

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        RaycastHit hit;

        vertSpeed = characterController.velocity.y;
       // Debug.Log(Physics.Raycast(transform.position, Vector3.down, out hit));
       groundedPlayer = false;

        if(vertSpeed <= 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            
            distanceToGround = characterController.height/0.95f;
            groundedPlayer = hit.distance <= distanceToGround;
        }

        //Handles sprinting
        playerSpeed = baseSpeed;
        int sprintSpeed = playerSpeed + sprintBoost;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = sprintSpeed;
           
        }
        else
        {
            playerSpeed = baseSpeed;

        }


        float deltaX = Input.GetAxis("Horizontal") * playerSpeed;
        float deltaZ = Input.GetAxis("Vertical") * playerSpeed;


        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {

            deltaY = jumpHeight;

        }

        if(!groundedPlayer && deltaY > terminalVelocity)
        {

            deltaY += gravityValue*5*Time.deltaTime; //Yes you reall need time.deltatime here too
        }

        if (groundedPlayer && deltaY < 0) 
        {
            deltaY = 0;
        }

        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float horizontalRot = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(0, horizontalRot, 0);


        move.x = deltaX;
        move.z = deltaZ;

        //Translate vector from global space to local
        move = transform.TransformDirection(move);

        //Add vertical direction
        move.y = deltaY;

        //Flying Element

        Vector3 point = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(point);

        RaycastHit fly_hit;


        if (Physics.Raycast(ray, out fly_hit, Mathf.Infinity, layerMask)) //<-- Add layerMask element here!
        {
            grappling_crosshair.enabled = true;
            default_crosshair.enabled = false;

            if (Input.GetKey("f"))
            {
                Vector3 playerPosition = transform.position;
                Vector3 flyToPoint = fly_hit.point;
                Vector3 flyDirection = flyToPoint - playerPosition;

                flyDirection *= flySpeed;
                move = flyDirection;

            }


        }
        else
        {
            grappling_crosshair.enabled = false;
            default_crosshair.enabled = true;

        }

        

        move *= Time.deltaTime;

        characterController.Move(move);


    }


}
