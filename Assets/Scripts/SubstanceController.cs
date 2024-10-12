using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceController : MonoBehaviour
{
    public ParticleSystem particle;
    private bool isEmissionEnabled = false;

    void Update()
    {
        if (transform.up.y < 0)
        {
            if (!isEmissionEnabled)
            {
                var emission = particle.emission;
                emission.enabled = true;
                isEmissionEnabled = true;
            }
        }
        else
        {
            if (isEmissionEnabled)
            {
                var emission = particle.emission;
                emission.enabled = false;
                isEmissionEnabled = false;
            }
        }
    }
}
