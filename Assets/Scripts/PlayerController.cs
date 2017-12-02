using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public InputController input;
    public Transform head;
    public GrabZoneController grabZone;

    public float forwardSpeed = 10.0f;
    public float lateralSpeed = 10.0f;
    public float lookUpSpeed = 50.0f;
    public float lookRightSpeed = 50.0f;
    public float lookUpMin = -90.0f;
    public float lookUpMax = 90.0f;

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_outlinedObjects = new List<cakeslice.Outline>();
	}
	
	void Update ()
    {
        // RESET OUTLINES
        foreach(cakeslice.Outline outlinedObject in m_outlinedObjects)
        {
            outlinedObject.enabled = false;
        }
        m_outlinedObjects.Clear();

        // RELEASE GRAB
        if (input.IsGrabReleased())
        {
            if (m_grabbedObject != null)
            {
                DropObject();
            }
        }

        
        if (m_grabbedObject == null)
        {
            List<GameObject> grabbableObjects = new List<GameObject>();
            grabZone.GetGrabbableObjects(ref grabbableObjects);
            
            if (grabbableObjects.Count != 0)
            {
                Rigidbody rb = grabbableObjects[0].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (input.IsGrabPressed())
                    {
                        GrabObject(rb);
                    }
                    else
                    {
                        cakeslice.Outline outline = rb.GetComponent<cakeslice.Outline>();
                        outline.enabled = true;
                        m_outlinedObjects.Add(outline);
                    }
                }
            }
        }
	}

    private void FixedUpdate()
    {
        if (input.IsLookEnabled())
        {
            // LOOKUP
            {
                Vector3 headEuler = head.localEulerAngles;
                float lookUp = input.GetLookUpAxis();
                headEuler.x += lookUp * lookUpSpeed * Time.fixedDeltaTime;
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
                euler.y += input.GetLookRightAxis() * lookRightSpeed * Time.fixedDeltaTime;
                transform.eulerAngles = euler;
            }
        }

        Vector3 velocity = new Vector3();
        velocity += transform.forward * input.GetForwardAxis() * forwardSpeed;
        velocity += transform.right * input.GetRightAxis() * lateralSpeed;
        velocity.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = velocity;

        if (m_grabbedObject != null)
        {
            Vector3 newPosition = grabZone.transform.TransformPoint(m_grabbedObjectPosition);
            //m_grabbedObject.MovePosition(newPosition);
            m_grabbedObject.velocity = (newPosition - m_grabbedObject.transform.position) / Time.fixedDeltaTime;

            Quaternion newRotation = Quaternion.LookRotation(grabZone.transform.TransformDirection(m_grabbedObjectForward), grabZone.transform.TransformDirection(m_grabbedObjectUp));
            m_grabbedObject.MoveRotation(newRotation);
        }
    }

    void GrabObject(Rigidbody _object)
    {
        if (m_grabbedObject != null)
        {
            Debug.LogError("Object already grabbed.");
            return;
        }

        m_grabbedObject = _object;

        m_grabbedObject.useGravity = false;

        m_grabbedObjectUp = grabZone.transform.InverseTransformDirection(m_grabbedObject.transform.up);
        m_grabbedObjectForward = grabZone.transform.InverseTransformDirection(m_grabbedObject.transform.forward);
        m_grabbedObjectPosition = grabZone.transform.InverseTransformPoint(m_grabbedObject.transform.position);
    }

    void DropObject()
    {
        m_grabbedObject.useGravity = true;

        m_grabbedObject = null;
    }

    Vector3 m_grabbedObjectUp;
    Vector3 m_grabbedObjectForward;
    Vector3 m_grabbedObjectPosition;
    Rigidbody m_grabbedObject;
    Rigidbody m_rigidbody;

    List<cakeslice.Outline> m_outlinedObjects;
}
