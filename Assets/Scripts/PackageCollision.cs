using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCollision : MonoBehaviour
{    
    public AudioSource source
    {
        get
        {
            return gameObject.GetComponent<AudioSource>();
        }
    }

    public float noImpactThreshold;
    public float lowImpactThreshold;
    public float highImpactThreshold;

    void OnCollisionEnter(Collision col)
    {
        float _impactForce = gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        Debug.Log(_impactForce);
        if (_impactForce <= noImpactThreshold)
        {
            // No Impact
            return;
        }
        else if (_impactForce > noImpactThreshold && _impactForce <= lowImpactThreshold)
        {
            // Soft Impact
            source.PlayOneShot(AudioManager.Instance.packageSoftImpacts, Random.Range(0f, 0.2f));
            return;
        }
        else if (_impactForce > lowImpactThreshold && _impactForce < highImpactThreshold)
        {
            // Mid Impact
            source.PlayOneShot(AudioManager.Instance.packageMidImpacts, Random.Range(0.2f, 0.5f));
            return;
        }
        else
        {
            // High Impact
            source.PlayOneShot(AudioManager.Instance.packageHighImpact, Random.Range(0.5f, 1f));

            return;
        }

    }

}
