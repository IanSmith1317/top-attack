using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rpgShooter : MonoBehaviour
{
    private CharacterController characterController;
    public GameObject rpg_warhead;
    public float rpg_thrust = 10000.0f;
    public float rpg_mass = 1.0f;
    public Camera playerCamera;

    private GameObject active_warhead;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            fire_rocket(rpg_warhead);
        }

        if(Input.GetKeyDown(KeyCode.R) && !canShoot)
        {
            reload();
        }
    }

    void instantiate_rpg_rigidbody(GameObject rpg_warhead, float rpg_mass)
    {
        Rigidbody rpg_rigid_body = rpg_warhead.AddComponent<Rigidbody>();
        rpg_rigid_body.mass = rpg_mass;

    }
    void fire_rocket(GameObject rpg_warhead)
    {
        //Get placeholder position & rotation
        Vector3 placeholder_pos = rpg_warhead.transform.position;
        Quaternion placeholder_rot = rpg_warhead.transform.rotation;

        active_warhead = Instantiate(rpg_warhead, placeholder_pos, placeholder_rot);
        //remove placeholder RPG
        rpg_warhead.SetActive(false);
        instantiate_rpg_rigidbody(active_warhead, rpg_mass);
        active_warhead.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward.normalized * rpg_thrust);
        canShoot = false;
    }

    void reload()
    {
        rpg_warhead.SetActive (true);
        canShoot = true;
    }
}
