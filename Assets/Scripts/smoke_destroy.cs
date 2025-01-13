using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_destroy : MonoBehaviour
{
    private ParticleSystem smoke_system;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            smoke_system = gameObject.GetComponent<ParticleSystem>();
            smoke_system.Stop();
            StartCoroutine(DestroyAfterParticles(1.0f));
        }

    }

    private IEnumerator DestroyAfterParticles(float timer)
    {

        // Wait for the timer
        yield return new WaitForSeconds(timer);

        if (smoke_system != null && smoke_system.IsAlive())
        {
            // Wait until the particle system is completely finished
            yield return new WaitWhile(() => smoke_system.IsAlive());
        }

        Destroy(gameObject);        

    }

}
