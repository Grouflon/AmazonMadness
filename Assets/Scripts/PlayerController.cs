using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public InputController input;
    public Transform head;

    public float forwardSpeed = 10.0f;
    public float lateralSpeed = 10.0f;
    public float lookUpSpeed = 50.0f;
    public float lookRightSpeed = 50.0f;
    public float lookUpMin = -90.0f;
    public float lookUpMax = 90.0f;

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (input.IsLookEnabled())
        {
            // LOOKUP
            {
                Vector3 headEuler = head.localEulerAngles;
                float lookUp = input.GetLookUpAxis();
                headEuler.x += lookUp * lookUpSpeed * Time.deltaTime;
                float positiveMin = (360.0f + lookUpMin);
                float halfForbiddenVAlue = lookUpMax + (positiveMin - lookUpMax) * 0.5f;

                if (headEuler.x > lookUpMax && headEuler.x < positiveMin)
                {
                    if (headEuler.x < halfForbiddenVAlue)
                    {
                        headEuler.x = lookUpMax;
                    }
                    else
                    {
                        headEuler.x = lookUpMin;
                    }
                }

                head.localEulerAngles = headEuler;
            }

            // TURN
            {
                Vector3 euler = transform.eulerAngles;
                euler.y += input.GetLookRightAxis() * lookRightSpeed * Time.deltaTime;
                transform.eulerAngles = euler;
            }
        }

        Vector3 velocity = new Vector3();
        velocity += transform.forward * input.GetForwardAxis() * forwardSpeed;
        velocity += transform.right * input.GetRightAxis() * lateralSpeed;
        velocity.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = velocity;
	}

    Rigidbody m_rigidbody;
}
