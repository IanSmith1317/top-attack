using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rpgShooter : MonoBehaviour
{
    private CharacterController characterController;
    public GameObject rpg_warhead;
    public float rpg_thrust = 20.0f;
    public float rpg_mass = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Check to see if there's a warhead
            if (rpg_warhead != null)
            {
                //Detach war head from parent
                rpg_warhead.transform.parent = null;

                instantiate_rpg_rigidbody(rpg_warhead, rpg_mass);

                rpg_warhead.GetComponent<Rigidbody>().AddForce(transform.forward * rpg_thrust);
            }
        }
    }

    void instantiate_rpg_rigidbody(GameObject rpg_warhead, float rpg_mass)
    {
        Rigidbody rpg_rigid_body = rpg_warhead.AddComponent<Rigidbody>();
        rpg_rigid_body.mass = rpg_mass;

    }
}
