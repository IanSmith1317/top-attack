using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warhead_controller : MonoBehaviour
{
    private GameObject smoke_trail;
    private GameObject explosion;
    private TrailRenderer color_trail;

    void Start()
    {
        // Enable smoke effect if the RPG becomes detached from its parent
        if (gameObject.transform.parent == null)
        {
            //smoke_trail = gameObject.transform.GetChild(0).gameObject;
            color_trail = GetComponent<TrailRenderer>();
            color_trail.enabled = true;
            explosion = gameObject.transform.GetChild(0).gameObject;


            //smoke_trail.SetActive(true);
        }
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
//smoke_trail.transform.parent = null;
        explosion.transform.parent = null;
        explosion.SetActive(true);

        Destroy(gameObject);
    }

}

