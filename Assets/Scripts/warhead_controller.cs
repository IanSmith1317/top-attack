using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warhead_controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Enable smoke effect if the RPG become detached from its parent
        if(gameObject.transform.parent == null)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

        };

    }
}
