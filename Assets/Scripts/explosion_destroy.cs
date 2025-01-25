using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class explosion_destroy : MonoBehaviour
{
    private ParticleSystem explosion;

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(DestroyAfterTime(5.0f));
    }

    private IEnumerator DestroyAfterTime(float timer)
    {

        // Wait for the timer
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);

    }


}
