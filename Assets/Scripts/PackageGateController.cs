using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageGateController : MonoBehaviour
{
    public Transform objectiveDisplayTransform;
    public PackageController packagePrefab;

    public float spinSpeed = 20.0f;
    public float viewAngleAmplitude = 20.0f;
    public float viewAnglePhase = 3.0f;
    public float wiggleAmplitude = 1.0f;
    public float wigglePhase = 1.0f;

    public delegate void PackageGateDelegate(PackageGateController _gate, PackageController _package);

    public event PackageGateDelegate OnValidPackage;
    public event PackageGateDelegate OnInvalidPackage;

    public void SetObjectiveEnabled(bool _enabled)
    {
        m_objectiveEnabled = _enabled;
        if (m_objective != null)
        {
            m_objective.enabled = m_objectiveEnabled;
        }
    }

    public bool IsObjectiveEnabled()
    {
        return m_objectiveEnabled;
    }

    public void SetObjective(PackageShape _shape, PackageColor _color, PackageLayout _layout)
    {
        m_objective.SetShape(_shape);
        m_objective.SetColor(_color);
        m_objective.SetLayout(_layout);
    }

	// Use this for initialization
	void Start ()
    {
        m_objective = Instantiate(packagePrefab, objectiveDisplayTransform);
        m_objective.gameObject.layer = 0;
        Rigidbody rb = m_objective.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        m_objective.gameObject.GetComponent<Collider>().enabled = false;
        m_objective.enabled = m_objectiveEnabled;
        m_objective.transform.localPosition = Vector3.zero;
        m_objective.outline.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_objectiveEnabled)
        {
            m_timer += Time.deltaTime;
            m_timer = m_timer % (viewAnglePhase * wigglePhase);

            m_spinAngle += spinSpeed * Time.deltaTime;
            m_spinAngle = m_spinAngle % 360.0f;
            float viewAngle = ((Mathf.Sin(Mathf.PI * 2.0f * m_timer / viewAnglePhase) * 2.0f) - 1.0f) * viewAngleAmplitude;
            m_objective.transform.rotation = Quaternion.AngleAxis(m_spinAngle, Vector3.up) * Quaternion.AngleAxis(viewAngle, Vector3.right);

            float wiggle = 1.0f + ((Mathf.Sin(Mathf.PI * 2.0f * m_timer / wigglePhase) * 2.0f) - 1.0f) * wiggleAmplitude;
            m_objective.transform.localScale = Vector3.one * wiggle;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_objectiveEnabled)
            return;

        PackageController package = other.GetComponent<PackageController>();
        if (package && package.canValidateWinCondition)
        {
            if(package.GetShape() == m_objective.GetShape() && package.GetColor() == m_objective.GetColor() && package.GetLayout() == m_objective.GetLayout())
            {
                if (OnValidPackage != null) OnValidPackage(this, package); 
            }
            else
            {
                if (OnInvalidPackage != null) OnInvalidPackage(this, package);
            }

            package.PrettyDestroy();
            package.canValidateWinCondition = false;
        }
    }

    PackageController m_objective;
    bool m_objectiveEnabled = false;
    float m_timer = 0.0f;
    float m_spinAngle = 0.0f;
}
