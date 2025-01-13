using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warhead_controller : MonoBehaviour
{
    private GameObject smoke_trail;

    void Update()
    {
        // Enable smoke effect if the RPG becomes detached from its parent
        if (gameObject.transform.parent == null)
        {
            smoke_trail = gameObject.transform.GetChild(0).gameObject;
            smoke_trail.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        smoke_trail.transform.parent = null;
        Destroy(gameObject);
    }

}

