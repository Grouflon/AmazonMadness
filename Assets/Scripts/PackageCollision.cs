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

        //Debug.Log(_impactForce);

        if (_impactForce <= noImpactThreshold)
        {
            // No Impact
            return;
        }
        else if (_impactForce > noImpactThreshold && _impactForce <= lowImpactThreshold)
        {
            // Soft Impact
            AudioManager.Instance.Play("Package_Impact_Soft", this.transform.position);
            return;
        }
        else if (_impactForce > lowImpactThreshold && _impactForce < highImpactThreshold)
        {
            // Mid Impact
            AudioManager.Instance.Play("Package_Impact_Mid", this.transform.position);
            return;
        }
        else
        {
            // High Impact
            if (col.collider.name == "Player")
            {
                AudioManager.Instance.Play("Package_Impact_Mid", this.transform.position);
                return;
            }
            AudioManager.Instance.Play("Package_Impact_High", this.transform.position);
            return;
        }

    }

}
