using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -75.0f;
    public float maximumVert = 75.0f;

    public float minimumHor = -75.0f;
    public float maximumHor = 75.0f;

    private float verticalRot = 0;



    // Start is called before the first frame update
    void Start()
    {
        //Prevent physics engine from impacting player camera
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }


    }
    //
    //TODO" Remove Horizontal rotation as it's now covered in charactercontroller
    //

    // Update is called once per frame
    void Update()
    {
        if(axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0); //Horizontal rotation

        } else if(axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot , 0);
        }
        else
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            
            verticalRot  = Mathf.Clamp(verticalRot , minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            Debug.Log(delta);
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }
}
